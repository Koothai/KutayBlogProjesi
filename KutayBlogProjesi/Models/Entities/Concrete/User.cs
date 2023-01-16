using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using KutayBlogProjesi.Models.Entities.Abstract;
using System.Collections.Generic;

namespace KutayBlogProjesi.Models.Entities.Concrete
{
    [Table("Users")]
    public class User:BaseEntity
    {
        public User() { }

        public User(string email, string password)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Username = email.Substring(0,email.IndexOf("@"));
        }

        public User(int id, string username, string password,string email)
        {
            Id = id;
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
        public ICollection<Article> Articles { get; set; }

    }
}
