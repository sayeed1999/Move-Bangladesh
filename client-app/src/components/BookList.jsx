const books = [
  { id: 1, title: 'No reason to live in fear!', author: 'Sayeed' },
  { id: 2, title: 'No reason to die alone!', author: 'Sayeed' },
];

const BookList = () => {

  return (
    <div className="row">
      <div className="col-sm-12">
        <h1>Here are some public list of books</h1>
        <table className="table table-striped align-middle">
          <thead>
          <tr>
            <th>ID</th>
            <th>Title</th>
            <th>Author</th>
            <th>Action</th>
          </tr>
          </thead>
          <tbody>
          {books.map((book) => (
            <tr key={book.id}>
              <td>{book.title}</td>
              <td>{book.author}</td>
              <td>
                <button className="btn btn-xs btn-danger" onClick={() => {}}>
                  Delete Book
                </button>
              </td>
            </tr>
          ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default BookList
