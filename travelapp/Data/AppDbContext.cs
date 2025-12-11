using Microsoft.EntityFrameworkCore;
using travelapp.Models;

namespace travelapp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------------------------------------
            // 1. ADIM: ŞEHİRLER (CITIES)
            // -----------------------------------------------------------
            modelBuilder.Entity<City>().HasData(
                new City { CityId = 34, Name = "İstanbul", CityUrl = "/Pictures/kızkulesi.png" },
                new City { CityId = 6, Name = "Ankara", CityUrl = "/Pictures/anıtkabir.png" },
                new City { CityId = 35, Name = "İzmir", CityUrl = "/Pictures/saatkulesi.png" },
                new City { CityId = 26, Name = "Eskişehir", CityUrl = "/Pictures/eskişehir.png" },
                new City { CityId = 17, Name = "Çanakkale", CityUrl = "/Pictures/çanakkale.png" }
            );

            // -----------------------------------------------------------
            // 2. ADIM: MEKANLAR (PLACES)
            // Repo'daki ID ve CityId eşleşmelerine sadık kalınmıştır.
            // -----------------------------------------------------------
            modelBuilder.Entity<Place>().HasData(
                // İSTANBUL (34)
                new Place { PlaceId = 1, CityId = 34, Name = "Galata Kulesi", ImageUrl = "/Pictures/galata.png", Description = "Şehrin harika manzarası." },
                new Place { PlaceId = 2, CityId = 34, Name = "Kız Kulesi", ImageUrl = "/Pictures/kızkulesi.png", Description = "Boğazın incisi." },
                new Place { PlaceId = 3, CityId = 34, Name = "Sultan Ahmet Cami", ImageUrl = "/Pictures/sultanahmet.png", Description = "Tarihi yarımadanın kalbi." },
                new Place { PlaceId = 4, CityId = 34, Name = "Yerebatan Sarnıcı", ImageUrl = "/Pictures/yerebatan.png", Description = "Gizemli sarnıç." },

                // ANKARA (6)
                new Place { PlaceId = 5, CityId = 6, Name = "Anıtkabir", ImageUrl = "/Pictures/anıtkabir.png", Description = "Atamızın istirahatgahı." },
                new Place { PlaceId = 6, CityId = 6, Name = "Ulucanlar Cezaevi", ImageUrl = "/Pictures/ulucanlar.png", Description = "Tarihi cezaevi müzesi." },
                new Place { PlaceId = 7, CityId = 6, Name = "Kuğulu Park", ImageUrl = "/Pictures/kugulupark.png", Description = "Huzurlu bir park." },
                new Place { PlaceId = 8, CityId = 6, Name = "TBMM Müzesi", ImageUrl = "/Pictures/tbmm.png", Description = "Meclis tarihi." },

                // İZMİR (35)
                new Place { PlaceId = 9, CityId = 35, Name = "Saat Kulesi", ImageUrl = "/Pictures/saatkulesi.png", Description = "İzmir'in sembolü." },
                new Place { PlaceId = 10, CityId = 35, Name = "Efes Antik Kenti", ImageUrl = "/Pictures/efes.png", Description = "Antik tarih." },
                new Place { PlaceId = 11, CityId = 35, Name = "Tarihi Asansör", ImageUrl = "/Pictures/tarihiasansör.png", Description = "Manzara noktası." },

                // ESKİŞEHİR (26)
                new Place { PlaceId = 12, CityId = 26, Name = "Masal Şatosu", ImageUrl = "/Pictures/eskişehir.png", Description = "Masalsı bir yapı." },
                new Place { PlaceId = 13, CityId = 26, Name = "Odunpazarı Evleri", ImageUrl = "/Pictures/odunpazarı.png", Description = "Renkli tarihi evler." },
                new Place { PlaceId = 14, CityId = 26, Name = "Balmumu Müzesi", ImageUrl = "/Pictures/balmumu.png", Description = "Ünlülerin heykelleri." },
                new Place { PlaceId = 15, CityId = 26, Name = "Porsuk Çayı", ImageUrl = "/Pictures/porsuk.png", Description = "Gondol keyfi." },

                // ÇANAKKALE (17)
                new Place { PlaceId = 16, CityId = 17, Name = "Şehitler Abidesi", ImageUrl = "/Pictures/eskişehir.png", Description = "Saygı duruşu." }, // Repo'da resim eskisehir kalmış, düzeltebilirsin
                new Place { PlaceId = 17, CityId = 17, Name = "Deniz Müzesi", ImageUrl = "/Pictures/denizmuzesi.png", Description = "Denizcilik tarihi." },
                new Place { PlaceId = 18, CityId = 17, Name = "Assos Antik Kenti", ImageUrl = "/Pictures/assos.png", Description = "Antik felsefe kenti." },
                new Place { PlaceId = 19, CityId = 17, Name = "Truva Antik Kenti", ImageUrl = "/Pictures/truva.png", Description = "Efsanevi savaş kenti." }
            );

            // -----------------------------------------------------------
            // 3. ADIM: YORUMLAR (FEEDBACKS) - ID'LER KONTROL EDİLDİ VE BENZERSİZLEŞTİRİLDİ.
            // -----------------------------------------------------------
            modelBuilder.Entity<Feedback>().HasData(
                // İSTANBUL
                new Feedback { FeedbackId = 1, PlaceId = 1, Rating = 3, Comment = "İdare eder.", UserName = "Ziyaretçi 1", Date = DateTime.Now },
                new Feedback { FeedbackId = 2, PlaceId = 1, Rating = 2, Comment = "Çok fazla kalabalık vardı.", UserName = "Ziyaretçi 2", Date = DateTime.Now },
                new Feedback { FeedbackId = 3, PlaceId = 3, Rating = 4, Comment = "Etkileyici.", UserName = "Ziyaretçi 3", Date = DateTime.Now },
                new Feedback { FeedbackId = 4, PlaceId = 4, Rating = 4, Comment = "Görülmeli.", UserName = "Ziyaretçi 4", Date = DateTime.Now },

                // ANKARA
                new Feedback { FeedbackId = 5, PlaceId = 5, Rating = 5, Comment = "Hissiyatı çok yüksek.", UserName = "Vatandaş", Date = DateTime.Now },
                new Feedback { FeedbackId = 6, PlaceId = 5, Rating = 5, Comment = "Herkesin gitmesi gereken bir yer.", UserName = "Ahmet", Date = DateTime.Now },
                new Feedback { FeedbackId = 7, PlaceId = 6, Rating = 4, Comment = "Her şey çok gerçekçi.", UserName = "Mehmet", Date = DateTime.Now },
                new Feedback { FeedbackId = 8, PlaceId = 8, Rating = 4, Comment = "Güzel.", UserName = "Ayşe", Date = DateTime.Now },

                // İZMİR
                new Feedback { FeedbackId = 9, PlaceId = 10, Rating = 5, Comment = "Muazzam tarih.", UserName = "Turist", Date = DateTime.Now },
                new Feedback { FeedbackId = 10, PlaceId = 11, Rating = 2, Comment = "Çok fazla sıra bekleniyor.", UserName = "Fatma", Date = DateTime.Now },

                // ESKİŞEHİR
                new Feedback { FeedbackId = 11, PlaceId = 12, Rating = 3, Comment = "Çocuklar için iyi.", UserName = "Veli", Date = DateTime.Now },
                new Feedback { FeedbackId = 12, PlaceId = 13, Rating = 5, Comment = "Çok güzel!", UserName = "Gezgin", Date = DateTime.Now },
                new Feedback { FeedbackId = 13, PlaceId = 13, Rating = 4, Comment = "Tarihi dokusu harika.", UserName = "Tarihçi", Date = DateTime.Now },
                new Feedback { FeedbackId = 14, PlaceId = 14, Rating = 2, Comment = "Çok fazla sıra bekledik.", UserName = "Ziyaretçi", Date = DateTime.Now },
                new Feedback { FeedbackId = 15, PlaceId = 15, Rating = 5, Comment = "Romantik bir yürüyüş için ideal.", UserName = "Aşıklar", Date = DateTime.Now },

                // ÇANAKKALE
                new Feedback { FeedbackId = 16, PlaceId = 16, Rating = 5, Comment = "Duygusal.", UserName = "Mehmetçik", Date = DateTime.Now },
                new Feedback { FeedbackId = 17, PlaceId = 16, Rating = 4, Comment = "Bakımlı.", UserName = "Ziyaretçi", Date = DateTime.Now },
                new Feedback { FeedbackId = 18, PlaceId = 17, Rating = 2, Comment = "Geliştirilmeli.", UserName = "Eleştirmen", Date = DateTime.Now },
                new Feedback { FeedbackId = 19, PlaceId = 17, Rating = 3, Comment = "Beklentinin altında bir yer.", UserName = "Ziyaretçi", Date = DateTime.Now },
                new Feedback { FeedbackId = 20, PlaceId = 18, Rating = 3, Comment = "Yol biraz zorlu.", UserName = "Şoför", Date = DateTime.Now },
                new Feedback { FeedbackId = 21, PlaceId = 18, Rating = 4, Comment = "Manzara güzel.", UserName = "Fotoğrafçı", Date = DateTime.Now }
            );
        }
    }
}