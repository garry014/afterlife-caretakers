using afterlife_caretakers.CustomValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class Funeral
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Religion must be selected.")]
        public string Religion { get; set; }
        public Boolean RequireRites { get; set; }
        public string ConductOptions { get; set; }
        public string ReligiousPName { get; set; }
        public string ReligiousPOCName { get; set; }
        [RegularExpression(@"(6|8|9)\d{7}", ErrorMessage = "Must be a valid Singapore number without country code.")]
        public string ReligiousPOCNumber { get; set; }
        //[Required(ErrorMessage = "Location of wake must be selected.")]
        public string WakeLocationIn { get; set; }
        //[Required(ErrorMessage = "Postal Code must be filled in"), RegularExpression(@"^\d{6}$", ErrorMessage = "Postal code should only have 6 numbers.")]
        public string WakePostalCode { get; set; }
        [Required(ErrorMessage = "Casket design must be selected")]
        public Int16 CasketID { get; set; }
        [Required(ErrorMessage = "Wake duration must be selected")]
        public Int16 WakeDuration {get;set; }
        public string PhotoFramed { get; set; }
        public string PhotoAttire { get; set; }
        [Required(ErrorMessage = "Location of attire needs to be filled.")]
        public string LocationAttire { get; set; }
        [Required(ErrorMessage = "Number of guests must be selected")]
        public Int16 WakeGuestsExpected { get; set; }
        public Boolean HasMakeupServices { get; set; }
        public Boolean HasHairstylingServices { get; set; }
        public Boolean HasMobileToilet { get; set; }
        public Boolean HasBeverages { get; set; }
        public Boolean HasTibits { get; set; }
        public Boolean HasFridge { get; set; }
        public Boolean HasLunch { get; set; }
        public Boolean HasDinner { get; set; }
        public Boolean HasRegisterBook { get; set; }
        public Boolean HasMemorialFolders { get; set; }
        [Required]
        public string FuneralVechicle { get; set; }
        public Boolean HasMemorialService { get; set; }
        public Boolean HasBusCatering { get; set; }
        public Boolean HasFuneralCermony { get; set; }
        [Required(ErrorMessage = "Number of guests expected should be selected.")]
        public Int16 FGuestsExpected { get; set; }
        [Required(ErrorMessage = "Final resting place must be selected.")]
        public string FinalRestingPlace { get; set; }
        [Required(ErrorMessage = "Columbarium must be selected.")]
        public string ColumbariumName { get; set; }
        public string PlaquePhoto { get; set; }
        public string PlaqueName { get; set; }
        public Boolean PlaqueHasBday { get; set; }
        public Boolean PlaqueHasDday { get; set; }
        public string PlaqueQuotes { get; set; }
        [Required(ErrorMessage ="Urn must be selected")]
        public Int16 UrnId { get; set; }
        public double PaymentAmount { get; set; }
        public Boolean HasExecutorVerifiedWake { get; set; }
        public int ExecutorID { get; set; }
        public int WillMaker_ID { get; set; }
        public string Signature { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public int LastUpdatedById { get; set; }
        public Boolean IsDeleted { get; set; }

        public void MapToModel(Funeral f)
        {
            f.RequireRites = RequireRites;
        }
    }

    public class OptionalImage
    {
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile Image { get; set; }
    }

    public class UrnReusePhoto
    {
        public Boolean isReused { get; set; }
    }
}
