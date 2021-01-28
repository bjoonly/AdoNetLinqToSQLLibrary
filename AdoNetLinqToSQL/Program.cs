using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetLinqToSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            //1
            // library.ShowBooks(library.AllBooksWitNumberOfPagesMoreThan(600));
            // Console.WriteLine(new string('-', 120));
            //2
            // library.ShowBooks(library.AllBooksWhoseNameBeginsWithCertainSymbol('d'));
            //  Console.WriteLine(new string('-', 120));
            //3
            //library.ShowBooks(library.AllBooksByAuthor("Mala", "Traviss"));
            // Console.WriteLine(new string('-', 120));
            //4
            //library.ShowBooks(library.AllBooksByAuthorsOfCertainCountryOrderByAlphabet("China"));
            //Console.WriteLine(new string('-', 120));
            //5
            //library.ShowBooks(library.AllBooksWithNameLengthLessThan(10));
            //Console.WriteLine(new string('-', 120));
            //6
            //library.ShowBook(library.BookWithMaxNumberOfPagesOfAuthorsOfCertainCountry("Poland"));
            // Console.WriteLine(new string('-', 120));
            //7
            //library.ShowAuthor(library.AuthorWithTheLeastNumberOfBooks());
            //Console.WriteLine(new string('-', 120));
            //8
            //Console.WriteLine(library.CountryWithTheMostNumberOfAuthors().Name); 
            //Console.WriteLine(new string('-', 120));
            string name, surname, country;
            int action, count;
            bool res;
            char symb;
            do
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Choose action:\n1.All books with number of pages more than\n2.All books whose name begins with a certain symbol" +
                        "\n3.All books by author\n4.All books by authors of certain country order by alphabet\n5.All books with name length less than" +
                        "\n6.Book with max number of pages of authors of certain country\n7.Author with the least number of books\n8.Country with the most number of authors\n9.Exit");
                    res = int.TryParse(Console.ReadLine(), out action);
                } while (!res);
                switch (action)
                {
                    case 1:
                        do
                        {
                            Console.Clear();
                            Console.Write("Enter count of page: ");
                            res = int.TryParse(Console.ReadLine(), out count);
                        } while (!res);
                        Console.Clear();
                        library.ShowBooks(library.AllBooksWitNumberOfPagesMoreThan(count));
                        Console.ReadLine();
                        break;
                    case 2:
                        do
                        {
                            Console.Clear();
                            Console.Write("Enter symbol: ");
                            res = Char.TryParse(Console.ReadLine(), out symb);
                        } while (!res);
                        Console.Clear();
                        library.ShowBooks(library.AllBooksWhoseNameBeginsWithCertainSymbol(symb));
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Console.Write("Enter name: ");
                        name = Console.ReadLine();
                        Console.Clear();
                        Console.Write("Enter surname: ");
                        surname = Console.ReadLine();
                        Console.Clear();
                        library.ShowBooks(library.AllBooksByAuthor(name, surname));
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.Clear();
                        Console.Write("Enter name of country: ");
                        country = Console.ReadLine();
                        Console.Clear();
                        library.ShowBooks(library.AllBooksByAuthorsOfCertainCountryOrderByAlphabet(country));
                        Console.ReadKey();
                        break;
                    case 5:
                        do
                        {
                            Console.Clear();
                            Console.Write("Enter length: ");
                            res = int.TryParse(Console.ReadLine(), out count);
                        } while (!res);
                        Console.Clear();
                        library.ShowBooks(library.AllBooksWithNameLengthLessThan(count));
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Clear();
                        Console.Write("Enter name of country: ");
                        country = Console.ReadLine();
                        Console.Clear();
                        library.ShowBook(library.BookWithMaxNumberOfPagesOfAuthorsOfCertainCountry(country));
                        Console.ReadKey();
                        break;
                    case 7:
                        Console.Clear();
                        library.ShowAuthor(library.AuthorWithTheLeastNumberOfBooks());
                        Console.ReadKey();
                        break;
                    case 8:
                        Console.Clear();
                        Console.WriteLine(library.CountryWithTheMostNumberOfAuthors().Name);
                        Console.ReadKey();
                        break;
                    case 9:
                        break;
                    default:
                        Console.WriteLine("Invalid operation.");
                        Console.ReadKey();
                        break;
                }
            } while (action != 9);
        }

    }
    class Library
    {
        LibraryDataContext context;
        public Library()
        {
            context = new LibraryDataContext();
        }
        //1. Метод повертає всі книги, кількість сторінок в яких більше за певне значення
        public IEnumerable<Books> AllBooksWitNumberOfPagesMoreThan(int countPages)
        {
            return context.Books.Where(b => b.Pages > countPages);
        }

        //2. Метод повертає всі книги, ім’я яких починається на літеру певну літеру, регістр не враховується
        public IEnumerable<Books> AllBooksWhoseNameBeginsWithCertainSymbol(char symb)
        {
            return context.Books.Where(b => b.Name[0] == Char.ToLower(symb) || b.Name[0] == char.ToUpper(symb));
        }

        //3. Метод повертає всі книги автора по імені та прізвищу
        public IEnumerable<Books> AllBooksByAuthor(string name, string surname)
        {
            return context.Books.Where(b => b.Authors.Name == name && b.Authors.Surname == surname);
        }
        //4. Метод повертає всі книги авторів певної країни розташованих в алфавітному порядку
        public IEnumerable<Books> AllBooksByAuthorsOfCertainCountryOrderByAlphabet(string country)
        {
            return context.Books.Where(b => b.Authors.Countries.Name == country).OrderBy(b => b.Name);

        }
        //5. Метод повертає всі книги, ім’я в яких складається менше ніж з N символів
        public IEnumerable<Books> AllBooksWithNameLengthLessThan(int countSymb)
        {
            return context.Books.Where(b => b.Name.Length < countSymb);
        }

        //6. Метод повертає книгу з максимальною кількістю сторінок авторів певної країни
        public Books BookWithMaxNumberOfPagesOfAuthorsOfCertainCountry(string country)
        {
            return context.Books.Where(b => b.Authors.Countries.Name == country).OrderByDescending(b => b.Pages).FirstOrDefault();
        }
        //7. Метод повертає автора, який має найменше книг в базі даних
        public Authors AuthorWithTheLeastNumberOfBooks()
        {
            return context.Authors.OrderBy(a => a.Books.Count()).FirstOrDefault();
        }
        //8. Метод повертає країну, авторів якої є найбільше в базі
        public Countries CountryWithTheMostNumberOfAuthors()
        {
            return context.Countries.OrderByDescending(c => c.Authors.Count()).FirstOrDefault();
        }
        public void ShowBooks(IEnumerable<Books> books)
        {
            Console.WriteLine("{0,-70} {1,15} {2,20}", "Name", "Count pages", "Author");
            Console.WriteLine(new string('-', 120));
            foreach (var item in books)
            {
                Console.WriteLine("{0,-70} {1,10} {2,30}", item.Name, item.Pages, item.Authors.Name + ' ' + item.Authors.Surname);

            }
        }
        public void ShowAuthor(Authors author)
        {
            Console.WriteLine("{0,-30} {1,20}", "Full name", "Country");
            Console.WriteLine(new string('-', 70));
            Console.WriteLine("{0,-27} {1,22} ", author.Name + ' ' + author.Surname, author.Countries.Name);
        }
        public void ShowBook(Books book)
        {
            Console.WriteLine("{0,-70} {1,15} {2,20}", "Name", "Count pages", "Author");
            Console.WriteLine("{0,-70} {1,10} {2,30}", book.Name, book.Pages, book.Authors.Name + ' ' + book.Authors.Surname);
        }
    }
}
    

