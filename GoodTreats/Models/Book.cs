using System.Collections.Generic;

namespace Library.Models
{
  public class Book
  {
    public Book()

    {
      this.JoinEntities = new HashSet<AuthorBook>();
    }
    public string Title { get; set; }
    public int BookId { get; set; }
    public string Synopsis { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<AuthorBook> JoinEntities { get; }
  }
}