using ItCommerce.Business.Entities;
using ItCommerce.Business.Extra;
using ItCommerce.Business.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf;
using DinkToPdf.Contracts;
using ItCommerce.Reporting.Reports;
using Microsoft.AspNetCore.Mvc;
using ItCommerce.Reporting.ViewModels;
using API.Business.Extra;
using ItCommerce.DTO.ModelDesign;

namespace ItCommerce.Api.Net.Models
{
    public interface IItCommerceRepository
    {
        //customers
        List<Customer> Customers();
        Customer CreateCustomer(Customer obj);
        Customer UpdateCustomer(Customer obj);
        Customer DeleteCustomer(int id);

        //prestations
        List<Prestation> Prestations();
        List<Prestation> Prestation(int id);
        Prestation UpdatePrestation(Prestation obj);
        Prestation CreatePrestation(Prestation obj);
        Prestation DeletePrestation(int id);

        //agencies
        List<Agency> Agencies(int id);
        Agency CreateAgency(Agency obj);
        Agency UpdateAgency(Agency obj);
        Agency DeleteAgency(int id);


        //categories
        List<Category> Categories(int id);
        Category CreateCategory(Category obj);
        Category UpdateCategory(Category obj);
        Category DeleteCategory(int id);

        //measure types
        List<MeasureType> MeasureTypes(int id);
        MeasureType CreateMeasureType(MeasureType obj);
        MeasureType UpdateMeasureType(MeasureType obj);
        MeasureType DeleteMeasureType(int id);

        //suppliers
        List<Supplier> Suppliers(int id);
        Supplier CreateSupplier(Supplier obj);
        Supplier UpdateSupplier(Supplier obj);
        Supplier DeleteSupplier(int id);

        //rayons
        List<Compartment> Compartments(int id);
        Compartment CreateCompartment(Compartment obj);
        Compartment UpdateCompartment(Compartment obj);
        Compartment DeleteCompartment(int id);

        //groups
        List<GroupHandler> Groups();
        List<Group> GroupsList();
        GroupHandler GroupLaws(int idGroup);
        Group CreateGroup(Group obj, List<Law> roles, int idProfil);
        Group UpdateGroup(Group obj, List<Law> roles, int idProfil);

        //roles
        List<Law> Roles();

        //groupe_roles
        List<Group_Laws> GroupRoles(int idGroup);

        //products
        List<Product> Products(int id);
        List<Product> SearchProducts(int id, string name, string code);
        List<ProductInStock> ProductsFromStock(int id);
        List<ProdMeasureType> ProductTypes(int id);
        Product CreateProduct(Product obj);
        Product UpdateProduct(Product obj);
        Product DeleteProduct(int id);
        Product DeleteProdMeasureType(ProdMeasureType obj);
        Product DeleteProdAssociation(MeasureAssociation obj);

        //orders
        List<Order> Orders(int id);
        List<SavedOrder> SavedOrders(int id);
        List<Order> OrdersNotCompleted(int id);
        Order CreateOrder(Order obj);
        SavedOrder CreateSavedOrder(SavedOrder obj);
        Order PayOrderBalance(Order obj);
        List<Order> SearchOrders(PeriodParam obj);
        SavedOrder DeleteSavedOrder(int id);


        //stock
        List<ProductInStock> StockView(int id);
        List<ProductInStock> SearchStock(int id, string name, string code);
        List<ProductInStock> StockViewForSale(int id);
        List<ProductInStock> StockViewForSaleLimitByName(int id, string name);
        List<ProductInStock> StockViewForSaleLimitByCode(int id, string code);
        ProductInStock UpdateStockView(ProductInStock obj, int id);
        ProductInStock ExpandStockView(ProductInStock obj, int id);
        List<ProductInStock> ProductStockView(PrmProdStkView ppsV);
        List<ProductInStock> StockExpired(PeriodParam obj, int id);
        List<ProductInStock> generateBarcodes(List<ProductInStock> list, int id);


        //sales
        List<ProductSale> Sales(int id);
        List<ProductSale> SearchSales(PeriodParam obj);
        List<ProductSale> Devis(int id);
        List<ProductSale> SearchDevis(PeriodParam obj);

        // string CreateSale(Sale obj, string destinationPath, string resourcePath);
        SaleDataView CreateSale(ProductSale _order);

        ProductSale PaySaleBalance(ProductSale obj);
        Invoice NormaliseInvoice(int id);
        AvoirDataView CancelInvoice(ProductSale obj);
        ProductSale CreateDevis(ProductSale obj);


        //services sales
        List<ServiceSale> ServiceSales(int id);
        List<ServiceSale> SearchServiceSales(PeriodParam obj);

        // string CreateServiceSale(Sale obj, string destinationPath, string resourcePath);
        Task<byte[]> CreateServiceSale(ServiceSale _order, ITemplateService _templateService, IConverter _converter);

        //cryptage de mot dee passe
        string CryptWord(string word);

        //salestarget
        List<SaleTarget> SaleTargetList(int id);
        List<SaleTarget> SaleTargetList(PeriodParam obj);
        SaleTarget CreateSaleTarget(SaleTarget obj);
        SaleTarget UpdateSaleTarget(SaleTarget obj);
        SaleTarget DeleteSaleTarget(int id);


        //users
        List<User> Agents(int idProfil);
        List<User> Customers(int idProfil);
        User CreateAgent(User obj);
        User UpdateAgent(User obj);
        User DeleteAgent(int id);
        utilisateur Verified(string key);
        utilisateur SearchUserWithEmail(string email);

        //profils
        List<Profil> Profiles(int idProfil);
        Profil CreateProfile(Profil obj);
        Profil UpdateProfile(Profil obj);
        Profil UpdateCompanyLogo(string login, string logo);
        Profil UpdateUserProfile(Profil obj);
        Company UpdateCompanyProfile(Company obj);
        Profil DeleteProfile(int id);
        Profil Signin(Profil obj);
        Licence SaveLicence(Licence obj);
        Licence UpdateLicence(Licence obj);
        string GetCurrentLicenceCode();
        Licence GetCurrentLicence();
        Profil GetActiveProfileByCode(string code);

        //stock limit
        List<ProdStockLimit> StockLimit(int id);
        ProdStockLimit CreateStockLimit(ProdStockLimit obj);
        ProdStockLimit UpdateStockLimit(ProdStockLimit obj);
        ProdStockLimit DeleteStockLimit(int id);
        List<ProductInStock> GetProdOnAlertList(int id);

        //transfer
        List<StockTransfer> StockTransfers(int id);
        StockTransfer CreateTransfer(StockTransfer obj);
        List<StockTransfer> SearchStockTransfers(PeriodParam obj);

        //report
        Order PrintOrder(Order obj);
        TransfertViewModel PrintTransfers(PeriodParam obj);
        StockTransfer PrintTransfer(StockTransfer obj);
        SaleDetailView PrintDevis(ProductSale obj);
        //FileObject PrintBarcodes(PeriodParam obj, string destinationPath, string resourcePath);

        //stats
        GlobalReport GetCurrentReport(Profil obj);
        GlobalReport GetPeriodGlobalReport(PeriodParam obj);
        GlobalReport GetProductPeriodGlobalReport(PeriodParam obj);
        List<ProductInStock> GetProductMostSoldByQtyReport(PeriodParam obj);
        List<ProductInStock> GetProductMostSoldByCaReport(PeriodParam obj);
        List<ProductInStock> GetProductMostSoldByProfitReport(PeriodParam obj);
        List<UserReport> GetSalesReportBySellers(PeriodParam obj);
        List<UserReport> GetSalesReportByCustomers(PeriodParam obj);


        //banks
        List<Bank> Banks(int id);
        Bank CreateBank(Bank obj);
        Bank UpdateBank(Bank obj);
        Bank DeleteBank(int id);

        //virtual account
        List<AccountType> AccountTypes(int idProfil);
        List<VirtualAccount> VirtualAccounts(int id);
        VirtualAccount CreateVirtualAccount(VirtualAccount obj);
        VirtualAccount UpdateVirtualAccount(VirtualAccount obj);
        VirtualAccount DeleteVirtualAccount(int id);

        //fin operations
        List<FinOperation> FinOperations(int idProfil);
        List<FinOperation> SearchFinOperations(PeriodParam obj);
        FinOperation CreateFinOperation(FinOperation obj);

        //logs
        List<LogItem> Logs(int id);
        List<LogItem> Logs(PeriodParam obj);
        List<string> ErrorLogs();

        //print files
        Order PrintOrderDetail(Order _order);
        SaleDetailView PrintSaleDetail(ProductSale _sale);
        AvoirDetailView PrintAvoirDetail(ProductSale _sale);
        SaleViewModel PrintSaleList(PeriodParam param);
        OrderViewModel PrintOrderList(PeriodParam param);
        StockViewModel PrintStockView(Profil obj);
        List<BarcodeViewModel> PrintBarcodes(PeriodParam obj);

        //param Mecef
        List<ParamMecef> ParamsMecef(int id);
        UpdateMecef UpdateParamMecef(UpdateMecef obj);

        //Jwt
        string GetMyName();

        //licence concern
         public List<ModuleRequest> GetModuleList();
         public List<FormuleRequest> GetFormuleList();
    }
}
