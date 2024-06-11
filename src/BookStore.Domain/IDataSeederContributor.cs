using BookStore.Authors;
using BookStore.Books;
using BookStore.Markers;
using BookStore.Stocks;
using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace BookStore
{
    public class BookStoreDataSeederContributor
    : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IStockRepository _stockRepository;
        private readonly AuthorManager _authorManager;
        private readonly IRepository<Marker, Guid> _markerRepository;

        public BookStoreDataSeederContributor(
            IRepository<Book, Guid> bookRepository,
            IAuthorRepository authorRepository,
            IStockRepository stockRepository,
            AuthorManager authorManager,
            IRepository<Marker, Guid> markerRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _authorManager = authorManager;
            _stockRepository = stockRepository;
            _markerRepository = markerRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepository.GetCountAsync() > 0)
            {
                return;
            }

            var orwell = await _authorRepository.InsertAsync(
                await _authorManager.CreateAsync(
                    "George Orwell",
                    new DateTime(1903, 06, 25),
                    "Orwell produced literary criticism and poetry, fiction and polemical journalism; and is best known for the allegorical novella Animal Farm (1945) and the dystopian novel Nineteen Eighty-Four (1949)."
                )
            );

            var douglas = await _authorRepository.InsertAsync(
                await _authorManager.CreateAsync(
                    "Douglas Adams",
                    new DateTime(1952, 03, 11),
                    "Douglas Adams was an English author, screenwriter, essayist, humorist, satirist and dramatist. Adams was an advocate for environmentalism and conservation, a lover of fast cars, technological innovation and the Apple Macintosh, and a self-proclaimed 'radical atheist'."
                )
            );

            var book1 = await _bookRepository.InsertAsync(
                new Book
                {
                    AuthorId = orwell.Id, // SET THE AUTHOR
                    Name = "1984",
                    Type = BookType.Dystopia,
                    PublishDate = new DateTime(1949, 6, 8),
                    Price = 19.84f
                },
                autoSave: true
            );

            var book2 = await _bookRepository.InsertAsync(
                new Book
                {
                    AuthorId = douglas.Id, // SET THE AUTHOR
                    Name = "The Hitchhiker's Guide to the Galaxy",
                    Type = BookType.ScienceFiction,
                    PublishDate = new DateTime(1995, 9, 27),
                    Price = 42.0f
                },
                autoSave: true
            );

            await _bookRepository.InsertAsync(
                new Book
                {
                    AuthorId = douglas.Id, // SET THE AUTHOR
                    Name = "The Hitchhiker's Guide to the Galaxy 22",
                    Type = BookType.ScienceFiction,
                    PublishDate = new DateTime(1995, 9, 27),
                    Price = 42.0f
                },
                autoSave: true
            );

            await _stockRepository.InsertAsync(
                new Stock
                {
                    Quantity = 4,
                    BookId = book1.Id
                }, autoSave: true
            );

            await _stockRepository.InsertAsync(
                new Stock
                {
                    BookId = book2.Id,
                    Quantity = 5,
                }, autoSave: true
            );

            await _markerRepository.InsertAsync(
                new Marker
                {
                    Latitude = 40.43284224867512,
                    Longitude = -3.6797333304939044,
                    Name = "Telepizza",
                    MarkerType = MarkerType.restaurant
                }, autoSave: true
            );
            await _markerRepository.InsertAsync(
                new Marker
                {
                    Latitude = 40.40897967392807,
                    Longitude = -3.692601503506527,
                    Name = "Taco Bell",
                    MarkerType = MarkerType.restaurant
                }, autoSave: true
            );
            await _markerRepository.InsertAsync(
                new Marker
                {
                    Latitude = 40.401558192095614,
                    Longitude = -3.674119298007289,
                    Name = "Basic Fit",
                    MarkerType = MarkerType.gym
                }, autoSave: true
            );
        }
    }
}
