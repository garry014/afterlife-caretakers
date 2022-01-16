using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Models
{
    public class FuneralPricing
    {
        public int RitePlanned { get; set; }
        public int VoidDeck { get; set; }
        public int LandedProperty { get; set; }
        public int AYSParlour { get; set; }
        public int SinMingParlour { get; private set; }
        public int GardenParlour { get; private set; }
        public int TeoChewParlour { get; private set; }
        public int SGParlour { get; private set; }
        public int WGuestsExpected50 { get; private set; }
        public int WGuestsExpected100 { get; private set; }
        public int WGuestsExpected200 { get; private set; }
        public int WGuestsExpectedMT200 { get; private set; }
        public int Makeup { get; private set; }
        public int Hairstyling { get; private set; }
        public int MobileToilet { get; private set; }
        public int Beverages { get; private set; }
        public int Titbits { get; private set; }
        public int Fridge { get; private set; }
        public int Lunch { get; private set; }
        public int Dinner { get; private set; }
        public int RegisterBook { get; private set; }
        public int MemorialFolders { get; private set; }
        public int VanHearse { get; private set; }
        public int LimousineHearse { get; private set; }
        public int FloatHearse { get; private set; }
        public int FuneralCeremony { get; private set; }
        public int BusForVisitors { get; private set; }
        public int MemorialService { get; private set; }
        public int Govcolum { get; private set; }
        public int Cckcemetery { get; private set; }
        public int Seaburial { get; private set; }
        public int Religiouscolum { get; private set; }
        public int Inlandash { get; private set; }

        public FuneralPricing()
        {
            this.RitePlanned = 250;
            this.VoidDeck = 270;
            this.LandedProperty = 450;
            this.AYSParlour = 730;
            this.SinMingParlour = 750;
            this.GardenParlour = 790;
            this.TeoChewParlour = 820;
            this.SGParlour = 1100;
            this.WGuestsExpected50 = 300;
            this.WGuestsExpected100 = 500;
            this.WGuestsExpected200 = 1000;
            this.WGuestsExpectedMT200 = 2300;
            this.Makeup = 50;
            this.Hairstyling = 35;
            this.MobileToilet = 100;
            this.Beverages = 35;
            this.Titbits = 20;
            this.Fridge = 100;
            this.Lunch = 100;
            this.Dinner = 150;
            this.RegisterBook = 20;
            this.MemorialFolders = 80;
            this.VanHearse = 95;
            this.LimousineHearse = 225;
            this.FloatHearse = 550;
            this.FuneralCeremony = 350;
            this.BusForVisitors = 200;
            this.MemorialService = 150;
            this.Govcolum = 2550;
            this.Cckcemetery = 2440;
            this.Seaburial = 500;
            this.Religiouscolum = 3150;
            this.Inlandash = 450;
        }

        public double CalculateTotal(Funeral f)
        {
            FuneralPricing fp = new FuneralPricing();
            double sum = 0.0;
            // Rite planned
            if (f.ConductOptions == "planout")
            {
                sum = sum + (fp.RitePlanned * f.WakeDuration);
            }
            // Wake Location
            if (f.WakePostalCode == "575712")
            {
                sum = sum + (fp.AYSParlour * f.WakeDuration);
            }
            if (f.WakePostalCode == "575711")
            {
                sum = sum + (fp.SinMingParlour * f.WakeDuration);
            }
            if (f.WakePostalCode == "699815")
            {
                sum = sum + (fp.GardenParlour * f.WakeDuration);
            }
            if (f.WakePostalCode == "408609")
            {
                sum = sum + (fp.TeoChewParlour * f.WakeDuration);
            }
            if (f.WakePostalCode == "528746")
            {
                sum = sum + (fp.SGParlour * f.WakeDuration);
            }
            if (f.WakeLocationIn == "Landed Property")
            {
                sum = sum + (fp.LandedProperty * f.WakeDuration);
            }
            if (f.WakeLocationIn == "Void Deck")
            {
                sum = sum + (fp.VoidDeck * f.WakeDuration);
            }

            //Wake Plans
            switch (f.WakeGuestsExpected)
            {
                case 1:
                    sum = sum + fp.WGuestsExpected50;
                    break;
                case 2:
                    sum = sum + fp.WGuestsExpected100;
                    break;
                case 3:
                    sum = sum + fp.WGuestsExpected200;
                    break;
                case 4:
                    sum = sum + fp.WGuestsExpectedMT200;
                    break;
                default:
                    break;
            }

            // Wake Addons
            if (f.HasMakeupServices)
            {
                sum = sum + fp.Makeup;
            }
            if (f.HasHairstylingServices)
            {
                sum = sum + fp.Hairstyling;
            }
            if (f.HasMobileToilet)
            {
                sum = sum + (fp.MobileToilet * f.WakeDuration);
            }
            if (f.HasBeverages)
            {
                sum = sum + (fp.Beverages * f.WakeDuration);
            }
            if (f.HasTibits)
            {
                sum = sum + (fp.Titbits * f.WakeDuration);
            }
            if (f.HasFridge)
            {
                sum = sum + (fp.Fridge * f.WakeDuration);
            }
            if (f.HasLunch)
            {
                sum = sum + (fp.Lunch * f.WakeDuration);
            }
            if (f.HasDinner)
            {
                sum = sum + (fp.Dinner * f.WakeDuration);
            }
            if (f.HasRegisterBook)
            {
                sum = sum + fp.RegisterBook;
            }
            if (f.HasMemorialFolders)
            {
                sum = sum + fp.MemorialFolders;
            }

            // Funeral Plans
            switch (f.FuneralVechicle)
            {
                case "van hearse":
                    sum = sum + fp.VanHearse;
                    break;
                case "limousine hearse":
                    sum = sum + fp.LimousineHearse;
                    break;
                case "float hearse":
                    sum = sum + fp.FloatHearse;
                    break;
                default:
                    break;
            }
            if (f.HasFuneralCermony)
            {
                sum = sum + fp.FuneralCeremony;
            }
            if (f.HasBusCatering)
            {
                sum = sum + fp.BusForVisitors;
            }
            if (f.HasMemorialService)
            {
                sum = sum + fp.MemorialService;
            }

            //Final Resting Place
            switch (f.FinalRestingPlace)
            {
                case "Government Columbarium":
                    sum = sum + fp.Govcolum;
                    break;
                case "Chua Chao Kang Cemetery":
                    sum = sum + fp.Cckcemetery;
                    break;
                case "Sea Burial":
                    sum = sum + fp.Seaburial;
                    break;
                case "Religious Institution Columbarium":
                    sum = sum + fp.Religiouscolum;
                    break;
                case "Inland Ash Scattering Facility":
                    sum = sum + fp.Inlandash;
                    break;
                default:
                    break;
            }
            return sum;
        }
    }
}
