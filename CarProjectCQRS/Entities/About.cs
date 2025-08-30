namespace CarProjectCQRS.Entities
{
    public class About
    {
        public int AboutId { get; set; }

        // Genel başlık ve açıklama
        public string Title { get; set; }           // Örn: "Cental About"
        public string Description { get; set; }     // Üstteki paragraf

        // Vizyon
        public string VisionTitle { get; set; }     // "Our Vision"
        public string VisionDescription { get; set; }

        // Misyon
        public string MissionTitle { get; set; }    // "Our Mission"
        public string MissionDescription { get; set; }

        // Deneyim bilgisi
        public int YearsOfExperience { get; set; }  // 17

        // Deneyim maddeleri
        public string ExperienceItem1 { get; set; }
        public string ExperienceItem2 { get; set; }
        public string ExperienceItem3 { get; set; }
        public string ExperienceItem4 { get; set; }

        // Kurucu bilgileri
        public string FounderName { get; set; }     // "William Burgess"
        public string FounderTitle { get; set; }    // "Carveo Founder"
        public string FounderImageUrl { get; set; } // Profil fotoğrafı URL

        // Resimler
        public string MainImageUrl { get; set; }    // Ana görsel (Mercedes anahtarı)
        public string SecondaryImageUrl { get; set; } // Alt görsel (Yeşil VW araba)

        // Ortak alanlar
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }


}
