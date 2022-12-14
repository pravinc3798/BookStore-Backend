using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel bookModel);
        public BookModel EditBook(BookModel bookModel);
        public List<BookModel> GetAllBooks();
        public bool DeleteBook(int bookId);
        public BookModel GetBookById(int bookId);
    }
}
