using System;

namespace Domain.Models
{
  public class Post
  {
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public string Content { get; private set; }
    public string Author { get; private set; }
    public bool Visible { get; private set; }
    public DateTime? PublishedAt { get; private set; }
    public Guid FolderId { get; private set; }

    // Constructor for creating a new Post
    public Post(string title, DateTime createdDate, string content, string author, bool visible, Guid folderId)
    {
      Id = Guid.NewGuid();
      SetTitle(title);
      CreatedDate = createdDate;
      SetContent(content);
      SetAuthor(author);
      Visible = visible;
      FolderId = folderId;
    }

    // Methods to change the properties
    public void SetTitle(string title)
    {
      if (string.IsNullOrWhiteSpace(title))
        throw new ArgumentException("Title cannot be empty.", nameof(title));

      Title = title;
    }

    public void SetContent(string content)
    {
      if (string.IsNullOrWhiteSpace(content))
        throw new ArgumentException("Content cannot be empty.", nameof(content));

      Content = content;
    }

    public void SetAuthor(string author)
    {
      if (string.IsNullOrWhiteSpace(author))
        throw new ArgumentException("Author cannot be empty.", nameof(author));

      Author = author;
    }

    public void Publish(DateTime publishDate)
    {
      PublishedAt = publishDate;
      Visible = true;
    }

    // Additional methods for other business logic
  }
}
