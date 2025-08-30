namespace CarProjectCQRS.Entities
{
    public class About
    {
        public int AboutId { get; set; }

       
        public string Title { get; set; }          
        public string Description { get; set; }     

        
        public string VisionTitle { get; set; }    
        public string VisionDescription { get; set; }

       
        public string MissionTitle { get; set; }   
        public string MissionDescription { get; set; }

        //
        public int YearsOfExperience { get; set; }  // 17

       
        public string ExperienceItem1 { get; set; }
        public string ExperienceItem2 { get; set; }
        public string ExperienceItem3 { get; set; }
        public string ExperienceItem4 { get; set; }

       
        public string FounderName { get; set; }     
        public string FounderTitle { get; set; }    
        public string FounderImageUrl { get; set; } 
        // Resimler
        public string MainImageUrl { get; set; }    
        public string SecondaryImageUrl { get; set; } 

    
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }


}
