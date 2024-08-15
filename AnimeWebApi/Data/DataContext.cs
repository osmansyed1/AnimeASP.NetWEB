using AnimeWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimeWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        { 
            
        }

        public DbSet<Anime> animes { get; set; }
        public DbSet<Character> characters { get; set; }
        
        public DbSet<Director> directors { get; set; }  
        public DbSet<Viewer> viewers { get; set; }

        public DbSet<AnimeViewer>  animeviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           



            modelBuilder.Entity<AnimeViewer>()
                .HasKey(av => new { av.AnimeId, av.ViewerId });

            modelBuilder.Entity<AnimeViewer>()
                .HasOne(av => av.Anime)
                .WithMany(a => a.AnimeViewers)
                .HasForeignKey(av => av.AnimeId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<AnimeViewer>()
                .HasOne(av => av.Viewer)
                .WithMany(v => v.AnimeViewers)
                .HasForeignKey(av => av.ViewerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Director>().HasData(
                new Director { Id = 1, Name = "Jhon", achievement = "Golden Globe Awards" },
                new Director { Id = 2, Name = "Roy", achievement = "CFF" },
                new Director { Id = 3, Name = "Shawn", achievement = "CFF" },
                new Director { Id = 4, Name = "Ar Rhaman", achievement = "Freanch Award" },
                new Director { Id = 5, Name = "Roy", achievement = "Indian Award" }
                );

            modelBuilder.Entity<Character>().HasData(
                new Character { Id = 1, Name = "Naruto", Description = "Hero"},
                 new Character { Id = 2, Name = "Gojo", Description = "Protagonist" },
                  new Character { Id = 3, Name = "NaNami", Description = "Climax Hero" },
                   new Character { Id = 4, Name = "Madara Uchicha", Description = "Greatest Anagonist Ever" },
                    new Character { Id = 5, Name = "Shukuna", Description = "Notorious Vllain" },
                     new Character { Id = 6, Name = "Ivar The Boneless", Description = "Scary Villain & Hero" }
                );


            modelBuilder.Entity<Anime>().HasData(
               new Anime
               {
                   Id = 1,
                   Title = "Jujutsu Kaisen",
                   Genre = "Dark Adventure",
                   Details = "a panese manga series written and illustrated by Gege Akutami",
                   CharacterId = 1,
                   directorID = 1,
                   
               },
                new Anime
                {
                    Id = 2,
                    Title = "Naruto",
                    Genre = "Action, Adventure,Fantasy",
                    Details = "illustrated by Masashi Kishimoto. It tells the story of Naruto Uzumaki",
                    CharacterId = 2,
                    directorID = 2,
                },
                new Anime
                {
                    Id = 3,
                    Title = "Kingdom",
                    Genre = "Action,Diplomacy,Military",
                    Details = "In the Warring States Period of ancient China (475–221 BCE), Shin and Hyou are war-orphans in the kingdom of Qin",
                    CharacterId = 3,
                    directorID = 3,
                },
                new Anime
                {
                    Id = 4,
                    Title = "Hellsing Ultimate",
                    Genre = "Horror Adventure",
                    Details = "Hellsing, a British sponsored secret organisation, is in charge of keeping a check on all vampire",
                    CharacterId = 4,
                    directorID = 4,
                });

            modelBuilder.Entity<Viewer>().HasData(
                new Viewer { Id = 1, Name = "Elite", Critic = "rank 1" },
                new Viewer { Id = 2, Name = "Jackson", Critic = "rank 10" },
                new Viewer { Id = 3, Name = "Hinkle", Critic = "rank 111" }
                );

            modelBuilder.Entity<AnimeViewer>().HasData(
                new AnimeViewer { AnimeId = 1, ViewerId = 1 },
                new AnimeViewer { AnimeId = 1, ViewerId = 2 },
                new AnimeViewer { AnimeId = 2, ViewerId = 1 },
                new AnimeViewer { AnimeId = 2, ViewerId = 2 },
                new AnimeViewer { AnimeId = 2, ViewerId = 3 },
                new AnimeViewer { AnimeId = 3, ViewerId = 1 },
                new AnimeViewer { AnimeId = 3, ViewerId = 3 }
                );











        }
    }
}
