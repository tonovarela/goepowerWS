using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2.ViewModel.Goepower
{

    public class Order
    {
        public int OrderID { get; set; }
        public int WebUserID { get; set; }
        public int ProducerID { get; set; }
        public int CompanyID { get; set; }
        public int PartnerID { get; set; }
        public string OrderStatus { get; set; }
        public int SubStatus { get; set; }
        public double Price { get; set; }
        public string TaxName1 { get; set; }
        public double TaxRate1 { get; set; }
        public double Tax1 { get; set; }
        public string TaxName2 { get; set; }
        public double TaxRate2 { get; set; }
        public double Tax2 { get; set; }
        public double ShippingPrice { get; set; }
        public double BillingPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime BillingDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public DateTime CancelDate { get; set; }
        public int ShippingMethodID { get; set; }
        public string ShippingMethodName { get; set; }
        public string ShipType { get; set; }
        public int BillingMethodID { get; set; }
        public string BillingMethodName { get; set; }
        public string BillingMethodValue { get; set; }
        public int CostCentreID { get; set; }
        public string Note { get; set; }
        public int CouponID { get; set; }
        public double CouponAmount { get; set; }
        public double OrderFee { get; set; }
        public double MinInvoicePrice { get; set; }
        public string CompanyOrderFieldsXML { get; set; }
        public bool IsDirty { get; set; }
        public double OriginalTotalPrice { get; set; }
        public string DirtyMsgXML { get; set; }
        public int LocationID { get; set; }
        public string AddressIDs { get; set; }
        public int NotifyAddressID { get; set; }
        public string EmailToNotify { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ExternalInvoiceID { get; set; }
        public string InstructionFile { get; set; }
        public int SplitNo { get; set; }
        public string ReasonToCancel { get; set; }
        public string GatekeeperNote { get; set; }
        public string DealerNote { get; set; }
        public string CancelledByUserID { get; set; }
        public double PaidByOwnCredit { get; set; }
        public double TotalWithCreditDeducted { get; set; }
        public double PaidByGiftCards { get; set; }
        public string HiddenCCNo { get; set; }
        public string TrackNo { get; set; }
        public string ShipParty { get; set; }
        public int Container { get; set; }
        public int ShipmentID { get; set; }
        public string ReleasedByUserID { get; set; }
        public int DealerWebUserID { get; set; }
        public DateTime GatekeeperActionDate { get; set; }
        public string CurrencySym { get; set; }
        public int CurrencyID { get; set; }
        public int CardTypeID { get; set; }
        public int SplitOrderID { get; set; }
        public int ProductionFacilityID { get; set; }
        public string GoPrint2Key { get; set; }
        public string GatekeeperExternalEmail { get; set; }
        public int CreatedByWebUserID { get; set; }
        public string CreatedByUserName { get; set; }
        public int BillToPartnerID { get; set; }
        public double RushCharge { get; set; }
        public string DeliveryOption { get; set; }
        public DateTime ProductionDate { get; set; }
        public double RefundAmount { get; set; }
        public double ExtraCharge { get; set; }
        public int PayStatus { get; set; }
        public int OrderBitwiseSettings { get; set; }
        public bool DealerIsReleased { get; set; }
        public bool Archive { get; set; }
        public string GroupIDs { get; set; }
        public string CampaignName { get; set; }
        public int ProducerOrderID { get; set; }
        public int CompanyOrderID { get; set; }
        public string SpecialUse { get; set; }
    }

    public class Coupon
    {
        public string CouponName { get; set; }
        public string CouponNumber { get; set; }
        public double Amount { get; set; }
        public double ShippingAmount { get; set; }
        public string DateUsed { get; set; }
        public int CouponID { get; set; }
        public bool IsFreeShipping { get; set; }
    }

    public class User
    {
        public string Username { get; set; }
        public int ProducerID { get; set; }
        public int CompanyID { get; set; }
        public int WebUserID { get; set; }
        public string UserID { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Ext { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string TollFree { get; set; }
        public string Web { get; set; }
        public bool Active { get; set; }
        public double OwnCredit { get; set; }
        public double CreditLimit { get; set; }
        public double DefaultCreditLimit { get; set; }
        public bool IsAllowCompanyCredit { get; set; }
        public string Email { get; set; }
        public bool NotifyOnCancel { get; set; }
        public bool NotifyOnRelease { get; set; }
        public bool NotifyInProduction { get; set; }
        public bool NotifyInShipping { get; set; }
        public bool NotifyOnCompleted { get; set; }
        public bool NotifyIfGatekeeper { get; set; }
        public int AgencyID { get; set; }
        public int PartnerID { get; set; }
        public string ProductsString { get; set; }
        public string Password { get; set; }
        public bool ShipByPass { get; set; }
        public int ShipAddByPass { get; set; }
        public bool ShipAddBook { get; set; }
        public bool ShipManual { get; set; }
        public bool BillByPass { get; set; }
        public int ShippingOption { get; set; }
        public bool UseMyAccountData { get; set; }
        public bool GoToCheckout { get; set; }
        public string ProducerNote { get; set; }
        public string County { get; set; }
        public string ExternalID { get; set; }
        public int LocationID { get; set; }
        public string Culture { get; set; }
        public string AssociatedCompanyIDs { get; set; }
        public int CostCentreID { get; set; }
        public int CostCentreGroupID { get; set; }
        public int CatalogueXMLID { get; set; }
        public int TaxExempt { get; set; }
        public string Account { get; set; }
        public int BitwiseSettings { get; set; }
        public int StartupPage { get; set; }
        public int PriceDiscountID { get; set; }
        public string UserField1 { get; set; }
        public string UserField2 { get; set; }
        public int CalendarID { get; set; }
        public string Photo { get; set; }
        public int RolloutLocationID { get; set; }
        public string UserMessage { get; set; }
        public string FavProductIDs { get; set; }
    }

    public class Job
    {
        public int JobID { get; set; }
        public int OrderID { get; set; }
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int CustomID { get; set; }
        public string ProductType { get; set; }
        public string JobType { get; set; }
        public string JobName { get; set; }
        public string Thumbnail { get; set; }
        public int SetSize { get; set; }
        public int Quantity { get; set; }
        public int AgencyID { get; set; }
        public int ExtraChargeID { get; set; }
        public double Price { get; set; }
        public double SetupPrice { get; set; }
        public double TotalPrice { get; set; }
        public string GidgetIDs { get; set; }
        public string ExternalID { get; set; }
        public int JobStatus { get; set; }
        public bool IsJobSelected { get; set; }
        public string SKU { get; set; }
        public string OrderString { get; set; }
        public bool IsExtraCharged { get; set; }
        public string ExtraChargeType { get; set; }
        public int AddressID { get; set; }
        public int ImpositionID { get; set; }
        public string FileName { get; set; }
        public string GP2DownloadID { get; set; }
        public DateTime DateAdded { get; set; }
        public int CookieWebUserID { get; set; }
        public string TaxName1 { get; set; }
        public double TaxRate1 { get; set; }
        public double Tax1 { get; set; }
        public string TaxName2 { get; set; }
        public double TaxRate2 { get; set; }
        public double Tax2 { get; set; }
        public double TotalPriceWithTax { get; set; }
        public string TaxChars { get; set; }
        public int StationID { get; set; }
        public string StationName { get; set; }
        public int AssemblyID { get; set; }
        public int Records { get; set; }
        public string OtherIDs { get; set; }
        public bool IsGift { get; set; }
        public string TrackNo { get; set; }
        public int Container { get; set; }
        public int ShipmentID { get; set; }
        public string WCNumber { get; set; }
        public string ShipParty { get; set; }
        public DateTime DueDate { get; set; }
        public int DeliveryDays { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int ShippingDays { get; set; }
        public bool IsAutoDownloaded { get; set; }
        public int ReOrderJobID { get; set; }
        public string TrackUrl { get; set; }
        public int JobBitwiseSettings { get; set; }
        public int JobPriority { get; set; }
        public int PartnerIDFromData { get; set; }
        public double Savings { get; set; }
        public int SpecialCartWebUserID { get; set; }
        public string KitUniqueID { get; set; }
        public int KitProductID { get; set; }
        public int KitOrderNo { get; set; }
        public int KitParentJobID { get; set; }
    }

    public class More
    {
        public int ProductID { get; set; }
        public int TurnaroundID { get; set; }
        public object TurnaroundTitle { get; set; }
    }

    public class Product
    {
        public int ProductID { get; set; }
        public string UniqueID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Abstract { get; set; }
        public string SKU { get; set; }
        public int ProducerID { get; set; }
        public int CompanyID { get; set; }
        public string Filename { get; set; }
        public string ProductType { get; set; }
        public int BlockTypeID { get; set; }
        public string BlockAddressBook { get; set; }
        public int XMPieTypeID { get; set; }
        public bool IsAllowDBUpload { get; set; }
        public int JobTypeID { get; set; }
        public string ProductImage { get; set; }
        public string Thumbnail1 { get; set; }
        public string Thumbnail2 { get; set; }
        public DateTime DateCreated { get; set; }
        public double SetupPrice { get; set; }
        public int MinQty { get; set; }
        public int DefaultQty { get; set; }
        public int SetSize { get; set; }
        public string PriceShowMethod { get; set; }
        public bool IsDisplayPrice { get; set; }
        public bool IsShowPriceTable { get; set; }
        public bool IsShowTotal { get; set; }
        public bool IsShowAvailable { get; set; }
        public bool IsInventoried { get; set; }
        public int Inventory { get; set; }
        public int ReorderQty { get; set; }
        public double Weight { get; set; }
        public int PackageTypeID { get; set; }
        public int PickupTypeID { get; set; }
        public string InputStructure { get; set; }
        public string PreviewFile { get; set; }
        public string TemplateName { get; set; }
        public bool Active { get; set; }
        public int LargeThumbnailSize { get; set; }
        public string AssociateType { get; set; }
        public int ParameterAssociateID { get; set; }
        public bool IsReady { get; set; }
        public int ExtraChargeID { get; set; }
        public string ExtraChargeType { get; set; }
        public string SetupLabel { get; set; }
        public string ProductInfo { get; set; }
        public string PrinterNote { get; set; }
        public bool IsUsePriceSet { get; set; }
        public int PriceSetID { get; set; }
        public int ExtraDetailsPosition { get; set; }
        public string StockIDs { get; set; }
        public string InkIDs { get; set; }
        public string FinishIDs { get; set; }
        public string ForcedFonts { get; set; }
        public string PriceExtraTitle { get; set; }
        public string DownloadFile { get; set; }
        public bool IsDownloadable { get; set; }
        public int ProductGroupID { get; set; }
        public bool IsSpellCheck { get; set; }
        public string SpellCheckLanguages { get; set; }
        public int DataID { get; set; }
        public string ProductionFile { get; set; }
        public bool Tax1Exempt { get; set; }
        public bool Tax2Exempt { get; set; }
        public int DesignType { get; set; }
        public string ProductIDs { get; set; }
        public int BitwiseSettings { get; set; }
        public double Cost { get; set; }
        public int AssemblyID { get; set; }
        public int QtyDiscountID { get; set; }
        public int DeliveryDays { get; set; }
        public int QtyInBox { get; set; }
        public int InventoryID { get; set; }
        public int PackageID { get; set; }
        public int ClassificationSystemID { get; set; }
        public double Markup { get; set; }
        public int GlobalBootstrapProductLayoutID { get; set; }
        public int LinkToProductID { get; set; }
        public bool LinkToMandatory { get; set; }
        public double MinPrice { get; set; }
        public int DocPageCount { get; set; }
        public int FacilityID { get; set; }
        public string ColourMark { get; set; }
        public int ShippingProductGroupID { get; set; }
        public int ProductionOutputType { get; set; }
        public int ProductionOutputTypeDpi { get; set; }
        public int CreatedByWebUserID { get; set; }
        public double ShipPrice { get; set; }
        public More More { get; set; }
        public List<object> GetStocks { get; set; }
        public List<object> GetInks { get; set; }
        public List<object> GetFinishes { get; set; }
    }

    public class Shipaddress
    {
        public int OrderShipAddressID { get; set; }
        public int OrderID { get; set; }
        public int AddressID { get; set; }
        public string AddressName { get; set; }
        public int WebUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Ext { get; set; }
        public int ShippingMethodID { get; set; }
        public string Email { get; set; }
        public int ShipperID { get; set; }
        public int ServiceTypeID { get; set; }
        public string ServiceCode { get; set; }
        public string County { get; set; }
        public string ContactName { get; set; }
    }


   public  class ResponseGPViewModel
    {
        public Order order { get; set; }
        public bool HasCustomData { get; set; }
        public List<object> discounts { get; set; }
        public List<Coupon> coupons { get; set; }
        public List<object> giftcards { get; set; }
        public User user { get; set; }
        public List<Job> jobs { get; set; }
        public List<Product> products { get; set; }
        public Shipaddress shipaddress { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
        public int Count { get; set; }
    }
}
