namespace lesson
{
    /// <summary>
    /// Represents a single book
    /// </summary>
    internal class Book
    {
        /// <summary>
        /// The book full name, including the edition
        /// </summary>
        public string Name;

        /// <summary>
        /// The author full name
        /// </summary>
        public string Author;

        /// <summary>
        /// The year of publication of the book
        /// </summary>
        public int PublicationYear;

        /// <summary>
        /// A brief synopsis of the book (max 200 characters)
        /// </summary>
        public string Synopsis;

        /// <summary>
        /// The genres of the book
        /// </summary>
        public string[] genres;

        /// <summary>
        /// Creates a new instance of a Book
        /// </summary>
        /// <param name="Name">The book full name, including the edition</param>
        /// <param name="Author">The author full name</param>
        /// <param name="PublicationYear">The year of publication of the book</param>
        /// <param name="Synopsis">A brief synopsis of the book (max 200 characters)</param>
        /// <param name="genres">The genres of the book</param>
        public Book(
            string Name,
            string Author,
            int PublicationYear,
            string Synopsis,
            string[] genres
        )
        {
            // Quando prendiamo dei parametri in una qualsiasi funzione/metodo, vale il
            // principio della "programmazione paranoica" (dobbiamo controllare tutti i
            // parametri in ingresso e lanciare eccezioni qualora non siano validi)

            if (Name == null || Name == "")
            {
                throw new ArgumentException("Il nome del libro non può essere vuoto");
            }
            else if (Author == null || Author == "")
            {
                throw new ArgumentException("Il nome dell'autore non può essere vuoto");
            }
            else if (Synopsis == null || Synopsis == "")
            {
                throw new ArgumentException("La sinossi non può essere vuota");
            }
            else if (Synopsis.Length > 200)
            {
                throw new ArgumentException("La sinossi è troppo lunga (max 200 caratteri)");
            }
            else if (genres == null)
            {
                throw new ArgumentException("IL genere non può essere nullo");
            }

            // passate le guardie, è tutto ok
            this.Name = Name;
            this.Author = Author;
            this.PublicationYear = PublicationYear;
            this.Synopsis = Synopsis;
            this.genres = genres;
        }

        public override string ToString()
        {
            return $"\"{this.Name}\", \"{this.Author}\" ({this.PublicationYear})\n";
        }
    }

    /// <summary>
    /// This class allows to handle a library.
    /// This includes common operations such as booking and
    /// stocking book inventory.
    /// </summary>
    internal class Library
    {
        Book[] books;

        /// <summary>
        /// Creates a new instance of a Library, 
        /// with an empty inventory.
        /// </summary>
        public Library()
        {
            // Metodo costruttore: collegato alla parola chiave "new"
            // viene richiamato quando creo una nuova istanza.
            //
            // var prova = new Library(); // questo costruttore viene
            //                            // richiamato
            this.books = new Book[0];
        }

        /// <summary>
        /// Creates a new instance of a Library,
        /// with the specified book inventory.
        /// </summary>
        /// <param name="books">The books to add to the library</param>
        /// <exception cref="ArgumentNullException">If a null book arrray is passed</exception>
        public Library(Book[] books)
        {
            // Metodo costruttore (in overload): questo secondo costruttore
            // ha le stesse caratteristiche e proprietà del primo, e può
            // sostituirlo qualora ce ne fosse la necessità (ad esempio
            // per inizializzare direttamente i libri in inventario).

            // Essendo basato su parametri, essi vanno controllati e in caso
            // di errore, occorre lanciare delle eccezioni.
            if (books == null)
            {
                throw new ArgumentNullException("Non si può passare un array nullo di oggetti Book");
            }

            this.books = books;
        }

        /// <summary>
        /// Returns the books in the library.
        /// </summary>
        /// <returns>
        /// The book count
        /// </returns>
        public int BookCount()
        {
            // Metodo di istanza (è presente il this)
            //
            // Il this rappresenta "l'oggetto su cui è stato richiamato il metodo"
            //
            // Ad esempio se io facessi
            //
            // var prova = new Library();
            // Console.WriteLine(prova.BookCount());
            //
            // Il "this" si riferirebbe a "prova"
            //
            // (questo metodo in particolare si chiama
            // anche "metodo accessorio")
            return this.books.Length;
        }

        /// <summary>
        /// Returns the number of books of a particular author.
        /// </summary>
        /// <param name="author">The author to search books for</param>
        /// <returns>The count of the books made by the author</returns>
        public int BooksOfAuthor(string author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("Inserire un autore nullo non è permesso");
            }

            int count = 0;

            foreach (Book currentBook in this.books)
            {
                if (currentBook.Author == author)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Returns the number of books of a particular author, in the provided libraries.
        /// </summary>
        /// <param name="libraries">The libraries in which to search</param>
        /// <param name="author">The author to search books for</param>
        /// <returns>The count of the books made by the author</returns>
        public static int BooksOfAuthor(Library[] libraries, string author)
        {
            if (libraries == null)
            {
                throw new ArgumentNullException("Non si può inserire un array di librerie nullo");
            }
            else if (author == null)
            {
                throw new ArgumentNullException("Inserire un autore nullo non è permesso");
            }

            int count = 0;

            foreach(Library currentLibrary in libraries)
            {
                count += currentLibrary.BooksOfAuthor(author);
            }

            return count;
        }

        /// <summary>
        /// Returns the number of books published between the 2 provided years.
        /// </summary>
        /// <param name="yearFrom">The starting year</param>
        /// <param name="yearTo">The finishing year</param>
        /// <returns>The count of the books made between the 2 provided years</returns>
        public int BooksPublishedBetween(int yearFrom, int yearTo)
        {
            if (yearTo < yearFrom)
            {
                int temp = yearFrom;
                yearFrom = yearTo;
                yearTo = temp;
            }

            int count = 0;

            foreach(Book currentBook in this.books)
            {
                if (
                    currentBook.PublicationYear >= yearFrom &&
                    currentBook.PublicationYear <= yearTo
                )
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Returns the number of books published between the 2 provided years, in the provided libraries.
        /// </summary>
        /// <param name="libraries">The libraries in which to search</param>
        /// <param name="yearFrom">The starting year</param>
        /// <param name="yearTo">The finishing year</param>
        /// <returns>The count of the books made between the 2 provided years</returns>
        public static int BooksPublishedBetween(Library[] libraries, int yearFrom, int yearTo)
        {
            if (libraries == null)
            {
                throw new ArgumentNullException("Non si può inserire un array di librerie nullo");
            }
            else if (yearTo < yearFrom)
            {
                int temp = yearFrom;
                yearFrom = yearTo;
                yearTo = temp;
            }

            int count = 0;
            
            foreach(Library currentLibrary in libraries)
            {
                count += currentLibrary.BooksPublishedBetween(yearFrom, yearTo);
            }

            return count;
        }

        /// <summary>
        /// Returns the number of books of a particular genre
        /// </summary>
        /// <param name="genre">The book genre</param>
        /// <returns>The count of books of a particular genre</returns>
        public int BooksOfGenre(string genre)
        {
            if (genre == null)
            {
                throw new ArgumentNullException("Il genere nullo non è ammesso");
            }

            int count = 0;

            foreach(Book currentBook in this.books)
            {
                foreach(string currentGenre in currentBook.genres)
                {
                    if (currentGenre == genre)
                    {
                        count++;
                        break;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Returns the number of books of a particular genre, in the provided libraries
        /// </summary>
        /// <param name="libraries">The libraries in which to search</param>
        /// <param name="genre">The book genre</param>
        /// <returns>The count of books of a particular genre</returns>
        public static int BooksOfGenre(Library[] libraries, string genre)
        {
            if (libraries == null)
            {
                throw new ArgumentNullException("Non si può inserire un array di librerie nullo");
            }
            else if (genre == null)
            {
                throw new ArgumentNullException("Il genere nullo non è ammesso");
            }

            int count = 0;
            
            foreach(Library currentLibrary in libraries)
            {
                count += currentLibrary.BooksOfGenre(genre);
            }

            return count;
        }

        public override string ToString()
        {
            string result = "";

            // Libro[0]
            // - "Signore degli Anelli", "J.R.R. Tolkien" (1954)
            //   Sinossi:
            //   asojpdaspdoajsdpoajsdpoajsdpasojd
            // Libro[1]
            // - "Dracula", "Bram Stoker" (1897)
            //   Sinossi:
            //   aasodahsdioajdoiasd

            for (int i = 0; i < this.books.Length; i++)
            {
                //result += $"Libro[{i}]\n" +
                //    $"- \"{this.books[i].Name}\", \"{this.books[i].Author}\" ({this.books[i].PublicationYear})\n" +
                //    $"   Sinossi:\n" +
                //    this.books[i].Synopsis;

                //result += $"Libro[{i}]\n";
                //result += $"- \"{this.books[i].Name}\", \"{this.books[i].Author}\" ({this.books[i].PublicationYear})\n";
                //result += $"   Sinossi:\n";
                //result += this.books[i].Synopsis;

                result += $"Libro[{i}]\n" +
                    $"- {this.books[i]}\n" + // richiede di implementare ToString dentro Book
                    $"   Sinossi:\n" +
                    $"{this.books[i].Synopsis}\n";
            }

            return result;
        }
    }
}
