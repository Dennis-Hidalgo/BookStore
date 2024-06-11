using BookStore.Books;
using BookStore.EntityFrameworkCore;
using BookStore.Localization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bold = DocumentFormat.OpenXml.Spreadsheet.Bold;
using Border = DocumentFormat.OpenXml.Spreadsheet.Border;
using BottomBorder = DocumentFormat.OpenXml.Spreadsheet.BottomBorder;
using Color = DocumentFormat.OpenXml.Spreadsheet.Color;
using Font = DocumentFormat.OpenXml.Spreadsheet.Font;
using Fonts = DocumentFormat.OpenXml.Spreadsheet.Fonts;
using LeftBorder = DocumentFormat.OpenXml.Spreadsheet.LeftBorder;
using RightBorder = DocumentFormat.OpenXml.Spreadsheet.RightBorder;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
using TopBorder = DocumentFormat.OpenXml.Spreadsheet.TopBorder;

namespace BookStore.Documents
{
    public class DocumentAppService : BookStoreAppService, IDocumentAppService
    {
        private readonly BookStoreDbContext _context;
        private readonly IStringLocalizer<BookStoreResource> _languageTextManager;

        public DocumentAppService(BookStoreDbContext context, IStringLocalizer<BookStoreResource> languageTextManager)
        {

            _context = context;
            _languageTextManager = languageTextManager;
        }


        //private MemoryStream InsertMergeFieldsAsync(MemoryStream memoryStream)
        //{
        //    // Abrir el documento desde el stream en memoria
        //    using (var wordDocument = WordprocessingDocument.Open(memoryStream, true))
        //    {
        //        // Obtener todos los campos de fusión del documento
        //        var mergeFields = wordDocument.MainDocumentPart.RootElement.Descendants<FieldCode>()
        //            .Where(f => f.InnerText.Contains(" MERGEFIELD "));

        //        foreach (var mergeField in mergeFields)
        //        {
        //            var fieldName = mergeField.InnerText.Split('«', '»')[1]; // Obtener el nombre del campo de fusión

        //            // Encuentra el nodo Run que contiene el campo de fusión
        //            var run = mergeField.Ancestors<Run>().FirstOrDefault();

        //            // Encuentra el nodo Text dentro del Run (donde está el nombre del campo de fusión)
        //            var text = run?.Descendants<Text>().FirstOrDefault();

        //            if (text != null && (fieldName == "Test1" || fieldName == "Test2"))
        //            {
        //                // Si el campo de fusión es Test1 o Test2, asigna el valor correspondiente
        //                if (fieldName == "Test1")
        //                {
        //                    text.Text = "Dennis";
        //                }
        //                else if (fieldName == "Test2")
        //                {
        //                    text.Text = "Hidalgo";
        //                }
        //            }
        //        }


        //        wordDocument.MainDocumentPart.Document.Save(); // Guardar los cambios en el documento
        //    }

        //    var modifiedMemoryStream = new MemoryStream(memoryStream.ToArray());

        //    modifiedMemoryStream.Position = 0; // Restablecer la posición para la descarga
        //    return modifiedMemoryStream;
        //}

        private MemoryStream InsertMergeFieldsAsync(MemoryStream memoryStream)
        {
            memoryStream.Position = 0;  // Asegurarse de que el stream esté listo para leer desde el principio

            using (var wordDocument = WordprocessingDocument.Open(memoryStream, true))
            {
                
                var fieldCodes = wordDocument.MainDocumentPart.RootElement.Descendants<FieldCode>()
                    .Where(f => f.InnerText.Contains("MERGEFIELD"))
                    .ToList();  // Materializar la consulta
                Console.WriteLine("cantidad: " + fieldCodes.Count);

                foreach (var fieldCode in fieldCodes)
                {
                    var fieldName = ExtractFieldName(fieldCode.InnerText);
                    Console.WriteLine("texto:   <" + fieldName + ">");

                    // Asigna valores basados en el nombre del campo
                    if (fieldName == "AI_Auditoria_Cliente")
                    {
                        ReplaceMergeFieldWithText(fieldCode, "Nuevo valor para Text1");
                        Console.WriteLine("field: <" + fieldCode.InnerText + ">");
                    }
                    else if (fieldName == "AI_Auditoria_Nombre")
                    {
                        ReplaceMergeFieldWithText(fieldCode, "Nuevo valor para Text2");
                    }
                }

                wordDocument.MainDocumentPart.Document.Save();  // Guardar los cambios en el documento
            }

            memoryStream.Position = 0;  // Restablecer la posición para la descarga
            return memoryStream;
        }

        private string ExtractFieldName(string fieldCodeText)
        {
            // Extraer el nombre del campo, asumiendo que el formato es { MERGEFIELD Text1 \* MERGEFORMAT }
            var parts = fieldCodeText.Split(new[] { " MERGEFIELD ", " \\* " }, StringSplitOptions.RemoveEmptyEntries);
            return parts.Length >= 1 ? parts[0].Trim() : string.Empty;
        }

        private void ReplaceMergeFieldWithText(FieldCode fieldCode, string textValue)
        {
            // Obtener todos los Runs en el mismo párrafo que el FieldCode
            var paragraph = fieldCode.Ancestors<Paragraph>().FirstOrDefault();
            if (paragraph != null)
            {
                // Buscar todos los Text dentro del párrafo que contienen el formato de texto predeterminado de un MERGEFIELD
                var texts = paragraph.Descendants<Text>().Where(t => t.Text.Contains("«") || t.Text.Contains("»")).ToList();

                // Reemplazar el texto de cada nodo Text que forma parte del campo de fusión
                foreach (var text in texts)
                {
                    text.Text = textValue;  // Reemplazar el texto
                }
            }
        }


        private MemoryStream FillTableInWordDocumentAsync(MemoryStream memoryStream)
        {
            using (var wordDocument = WordprocessingDocument.Open(memoryStream, true))
            {
                // Buscar la primera tabla en el documento
                var table = wordDocument.MainDocumentPart.Document.Body.Descendants<Table>().FirstOrDefault();
                if (table != null)
                {
                    // Crear datos de prueba
                    string[,] tableData = {
                {_languageTextManager["Menu:BookStore"], "Edad"},
                {"Juan", "25"},
                {"María", "30"},
                {"Pedro", "28"}
            };

                    // Encontrar la fila que contiene las dos columnas
                    var targetRow = table.Elements<TableRow>().FirstOrDefault(row =>
                    {
                        var cells = row.Elements<TableCell>().ToList();
                        return cells.Count() == 2;
                    });

                    // Verificar si se encontró la fila objetivo
                    if (targetRow != null)
                    {
                        // Insertar nuevas filas debajo de la fila objetivo para los datos
                        for (int i = tableData.GetLength(0) - 1; i >= 0; i--)
                        {
                            var newRow = new TableRow();

                            // Insertar celdas con los datos correspondientes en cada columna
                            for (int j = 0; j < tableData.GetLength(1); j++)
                            {
                                var newCell = new TableCell();
                                var newParagraph = new Paragraph(new Run(new Text(tableData[i, j])));
                                newCell.Append(newParagraph);
                                newRow.Append(newCell);
                            }

                            // Insertar la nueva fila en la tabla después de la fila objetivo
                            table.InsertAfter(newRow, targetRow);
                        }
                    }
                }

                wordDocument.MainDocumentPart.Document.Save();  // Guardar los cambios en el documento
            }

            // Restablecer la posición para la descarga
            memoryStream.Position = 0;
            return memoryStream;
        }

        public MemoryStream GenerateExcelDocumentAsync()
        {
            // Obtener los nombres de las propiedades del modelo Book
            var propertyNames = typeof(Book).GetProperties().Select(p => p.Name).ToList();

            // Obtener datos de la tabla Books desde la base de datos
            var books = _context.Books.ToList();

            // Crear un nuevo flujo de memoria para almacenar el documento Excel
            var memoryStream = new MemoryStream();

            // Crear un documento Excel utilizando DocumentFormat.OpenXml
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
            {
                // Agregar una hoja de cálculo al documento
                var workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                // Agregar un WorkbookStylesPart para manejar los estilos
                var workbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                workbookStylesPart.Stylesheet = new Stylesheet();

                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                var sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
                var sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Books" };
                sheets.Append(sheet);

                var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Agregar encabezados de columna con formato
                var headerRow = new Row();
                foreach (var propertyName in propertyNames)
                {
                    headerRow.Append(CreateCellWithFormat(workbookPart, propertyName.ToUpper(), bold: true, backgroundColor: "FFFF00"));
                }
                sheetData.AppendChild(headerRow);

                // Llenar datos en el documento Excel
                foreach (var book in books)
                {
                    var dataRow = new Row();
                    foreach (var propertyName in propertyNames)
                    {
                        var propertyValue = typeof(Book).GetProperty(propertyName).GetValue(book);
                        dataRow.Append(CreateCellWithFormat(workbookPart, propertyValue?.ToString() ?? string.Empty));
                    }
                    sheetData.AppendChild(dataRow);
                }

                // Agregar bordes a todas las celdas de la tabla
                var allCells = sheetData.Descendants<Cell>();
                foreach (var cell in allCells)
                {
                    AddBorderToCell(workbookPart, cell);
                }

                workbookPart.Workbook.Save();
            }

            // Establecer la posición del flujo de memoria al principio
            memoryStream.Position = 0;

            // Devolver el flujo de memoria con el documento Excel
            return memoryStream;
        }

        private Cell CreateCellWithFormat(WorkbookPart workbookPart, string text, bool bold = false, string backgroundColor = null)
        {
            var cell = new Cell(new InlineString(new Text(text)));
            var cellFormat = new CellFormat();

            // Aplicar formato de negrita si es necesario
            if (bold)
            {
                cellFormat.FontId = 2;
                cellFormat.ApplyFont = true;
            }

            // Aplicar color de fondo si se proporciona
            if (!string.IsNullOrEmpty(backgroundColor))
            {
                var fills = workbookPart.WorkbookStylesPart.Stylesheet.Fills;
                var fillColor = new PatternFill(new ForegroundColor { Rgb = new HexBinaryValue { Value = backgroundColor } });
                fills.Append(fillColor);
                cellFormat.FillId = (uint)fills.Count - 1; // Obtener el índice del color de relleno
                cellFormat.ApplyFill = true;
            }

            var cellFormats = workbookPart.WorkbookStylesPart.Stylesheet.CellFormats;
            cellFormats.Append(cellFormat);

            return cell;
        }

        private void AddBorderToCell(WorkbookPart workbookPart, Cell cell)
        {
            var borders = workbookPart.WorkbookStylesPart.Stylesheet.Borders;

            // Crear bordes superior, izquierdo, derecho y inferior
            var border = new Border(
                new LeftBorder(new Color { Rgb = new HexBinaryValue { Value = "000000" } }) { Style = BorderStyleValues.Thin },
                new RightBorder(new Color { Rgb = new HexBinaryValue { Value = "000000" } }) { Style = BorderStyleValues.Thin },
                new TopBorder(new Color { Rgb = new HexBinaryValue { Value = "000000" } }) { Style = BorderStyleValues.Thin },
                new BottomBorder(new Color { Rgb = new HexBinaryValue { Value = "000000" } }) { Style = BorderStyleValues.Thin },
                new DiagonalBorder());

            borders.Append(border);

            // Obtener o agregar el índice del borde
            var cellFormat = cell.StyleIndex != null ?
                (CellFormat)workbookPart.WorkbookStylesPart.Stylesheet.CellFormats.ChildElements[int.Parse(cell.StyleIndex.InnerText)] :
                new CellFormat();

            cellFormat.BorderId = borders.Count - 1;
            cellFormat.ApplyBorder = true;

            var cellFormats = workbookPart.WorkbookStylesPart.Stylesheet.CellFormats;
            cellFormats.Append(cellFormat);
        }




        public MemoryStream GetDocumentAsync()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/docs", "WordPlantilla.docx");
            var memoryStream = new MemoryStream();
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                fileStream.CopyTo(memoryStream);
            }

            memoryStream.Position = 0;
            memoryStream = InsertMergeFieldsAsync(memoryStream);
            memoryStream.Position = 0;
            memoryStream = FillTableInWordDocumentAsync(memoryStream);
            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
