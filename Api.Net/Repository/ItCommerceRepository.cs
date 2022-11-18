using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItCommerce.Business.Entities;
using ItCommerce.DTO.ModelDesign;
using ItCommerce.Business.Actions;
using ItCommerce.Business.Query;
using ItCommerce.Reporting;
using ItCommerce.Business.Extra;
using DinkToPdf;
using DinkToPdf.Contracts;
using ItCommerce.Reporting.Reports;
using Microsoft.AspNetCore.Mvc;
using ItCommerce.Reporting.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using API.Business.Extra;

namespace ItCommerce.Api.Net.Models
{
    public class ItCommerceRepository : IItCommerceRepository
    {
        public IT_COMMERCEEntities _context;
        public ItCommerceRepository()
        {
            //_context = context;
            IOffice.ConnString = ConfigObject.ConnString;
            IOffice.DbInstance = ConfigObject.DbInstance;
        }
        #region customers
        List<Customer> IItCommerceRepository.Customers()
        {
            List<Customer> oList = OfficeParams.getListCustomers();
            return oList;
        }

        Customer IItCommerceRepository.CreateCustomer(Customer obj)
        {
            Customer result = OfficeParams.createCustomer(obj);
            return result;
        }

        Customer IItCommerceRepository.UpdateCustomer(Customer obj)
        {
            Customer result = OfficeParams.updateCustomer(obj);
            return result;
        }

        Customer IItCommerceRepository.DeleteCustomer(int id)
        {
            Customer result = OfficeParams.deleteCustomer(id);
            return result;
        }
        //*fin suppliers*//
        #endregion customers

        #region prestations
        List<Prestation> IItCommerceRepository.Prestations()
        {
            List<Prestation> oList = OfficeParams.getListPrestations();
            return oList;
        }

        List<Prestation> IItCommerceRepository.Prestation(int id)
        {
            List<Prestation> oList = OfficeParams.getPrestation(id);
            return oList;
        }

        Prestation IItCommerceRepository.CreatePrestation(Prestation obj)
        {
            Prestation result = OfficeParams.createPrestation(obj);
            return result;
        }

        Prestation IItCommerceRepository.UpdatePrestation(Prestation item)
        {
            Prestation result = OfficeParams.updatePrestation(item);
            return result;
        }

        Prestation IItCommerceRepository.DeletePrestation(int id)
        {
            Prestation result = OfficeParams.deletePrestation(id);
            return result;
        }
        //*fin suppliers*//
        #endregion prestations

        #region agency
        List<Agency> IItCommerceRepository.Agencies(int id)
        {
            List<Agency> oList = OfficeParams.getListAgencies(id);
            return oList;
        }

        //*agency*//
        Agency IItCommerceRepository.CreateAgency(Agency agency)
        {
            Agency result = OfficeParams.createAgency(agency);
            return result;
        }
        Agency IItCommerceRepository.UpdateAgency(Agency agency)
        {
            Agency result = OfficeParams.updateAgency(agency);
            return result;
        }
        Agency IItCommerceRepository.DeleteAgency(int id)
        {
            Agency result = OfficeParams.deleteAgency(id);
            return result;
        }
        //*agency*//
        #endregion agency

        #region category
        //*category*//
        List<Category> IItCommerceRepository.Categories(int id)
        {
            List<Category> oList = OfficeParams.getListCategories(id);
            return oList;
        }
        Category IItCommerceRepository.CreateCategory(Category item)
        {
            Category result = OfficeParams.createCategory(item);
            return result;
        }
        Category IItCommerceRepository.UpdateCategory(Category item)
        {
            Category result = OfficeParams.updateCategory(item);
            return result;
        }
        Category IItCommerceRepository.DeleteCategory(int id)
        {
            Category result = OfficeParams.deleteCategory(id);
            return result;
        }
        //*fin category*//
        #endregion category 

        #region MeasureType
        //*MeasureType*//
        List<MeasureType> IItCommerceRepository.MeasureTypes(int id)
        {
            List<MeasureType> oList = OfficeParams.getListMeasureTypes(id);
            return oList;
        }
        MeasureType IItCommerceRepository.CreateMeasureType(MeasureType item)
        {
            MeasureType result = OfficeParams.createMeasureType(item);
            return result;
        }
        MeasureType IItCommerceRepository.UpdateMeasureType(MeasureType item)
        {
            MeasureType result = OfficeParams.updateMeasureType(item);
            return result;
        }
        MeasureType IItCommerceRepository.DeleteMeasureType(int id)
        {
            MeasureType result = OfficeParams.deleteMeasureType(id);
            return result;
        }
        //*fin MeasureType*//
        #endregion measure type

        #region suppliers
        List<Supplier> IItCommerceRepository.Suppliers(int id)
        {
            List<Supplier> oList = OfficeParams.getListSuppliers(id);
            return oList;
        }
        Supplier IItCommerceRepository.CreateSupplier(Supplier item)
        {
            Supplier result = OfficeParams.createSupplier(item);
            return result;
        }
        Supplier IItCommerceRepository.UpdateSupplier(Supplier item)
        {
            Supplier result = OfficeParams.updateSupplier(item);
            return result;
        }
        Supplier IItCommerceRepository.DeleteSupplier(int id)
        {
            Supplier result = OfficeParams.deleteSupplier(id);
            return result;
        }
        //*fin suppliers*//
        #endregion suppliers

        #region compartments
        List<Compartment> IItCommerceRepository.Compartments(int id)
        {
            List<Compartment> oList = OfficeParams.getListCompartments(id);
            return oList;
        }
        Compartment IItCommerceRepository.CreateCompartment(Compartment item)
        {
            Compartment result = OfficeParams.createCompartment(item);
            return result;
        }
        Compartment IItCommerceRepository.UpdateCompartment(Compartment item)
        {
            Compartment result = OfficeParams.updateCompartment(item);
            return result;
        }
        Compartment IItCommerceRepository.DeleteCompartment(int id)
        {
            Compartment result = OfficeParams.deleteCompartment(id);
            return result;
        }
        //*fin compartments*//
        #endregion compartments


        #region products

        List<Product> IItCommerceRepository.Products(int id)
        {
            List<Product> oList = OfficeParams.getListProducts(id);
            return oList;
        }

        List<Product> IItCommerceRepository.SearchProducts(int id, string name, string code)
        {
            List<Product> oList = OfficeParams.searchProducts(id, name, code);
            return oList;
        }//SearchProducts

        List<ProductInStock> IItCommerceRepository.ProductsFromStock(int id)
        {
            List<ProductInStock> oList = OfficeParams.getListProductsFromStock(id);
            return oList;
        }

        List<ProdMeasureType> IItCommerceRepository.ProductTypes(int id)
        {
            List<ProdMeasureType> oList = OfficeParams.getListProductsMeasureTypes(id);
            return oList;
        }

        Product IItCommerceRepository.CreateProduct(Product item)
        {
            Product result = OfficeParams.createProduct(item);
            return result;
        }
        Product IItCommerceRepository.UpdateProduct(Product item)
        {
            Product result = OfficeParams.updateProduct(item);
            return result;
        }
        Product IItCommerceRepository.DeleteProduct(int id)
        {
            Product result = OfficeParams.deleteProduct(id);
            return result;
        }

        Product IItCommerceRepository.DeleteProdMeasureType(ProdMeasureType obj)
        {
            Product result = OfficeParams.deleteProdMeasureType(obj);
            return result;
        }

        Product IItCommerceRepository.DeleteProdAssociation(MeasureAssociation obj)
        {
            Product result = OfficeParams.deleteProdAssociation(obj);
            return result;
        }

        #endregion products


        #region groups

        //CreateGroup
        Group IItCommerceRepository.CreateGroup(Group obj, List<Law> roles, int idProfil)
        {
            Group result = OfficeParams.createGroup(obj, roles, idProfil);
            return result;
        }

        //UpdateGroup
        Group IItCommerceRepository.UpdateGroup(Group obj, List<Law> roles, int idProfil)
        {
            Group result = OfficeParams.updateGroup(obj, roles, idProfil);
            return result;
        }
        //ListGroupes
        List<GroupHandler> IItCommerceRepository.Groups()
        {
            List<GroupHandler> oList = OfficeParams.getListGroups();
            return oList;
        }

        //ListGroupes without Roles
        List<Group> IItCommerceRepository.GroupsList()
        {
            List<Group> oList = OfficeParams.getListGroupsWithoutRoles();
            return oList;
        }

        //GroupLaws
        GroupHandler IItCommerceRepository.GroupLaws(int idGroup)
        {
            GroupHandler oList = OfficeParams.getGroupRolesRef(idGroup);
            return oList;
        }

        #endregion

        #region groupe_roles
        List<Group_Laws> IItCommerceRepository.GroupRoles(int idGroup)
        {
            List<Group_Laws> oList = OfficeParams.getListGroupRoles(idGroup);
            return oList;
        }
        #endregion groupe_roles

        #region roles
        List<Law> IItCommerceRepository.Roles()
        {
            List<Law> oList = OfficeParams.getListRoles();
            return oList;
        }
        #endregion roles

        #region orders
        /// <summary>
        /// create order
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Order IItCommerceRepository.CreateOrder(Order item)
        {
            Order result = OfficeStock.createOrder(item);
            return result;
        }

        SavedOrder IItCommerceRepository.CreateSavedOrder(SavedOrder item)
        {
            SavedOrder result = OfficeStock.createSavedOrder(item);
            return result;
        }

        Order IItCommerceRepository.PayOrderBalance(Order item)
        {
            Order result = OfficeStock.payOrderBalance(item);
            return result;
        }


        List<Order> IItCommerceRepository.Orders(int id)
        {
            List<Order> oList = OfficeStock.getListOrders(id);
            return oList;
        }

        List<SavedOrder> IItCommerceRepository.SavedOrders(int id)
        {
            List<SavedOrder> oList = OfficeStock.getListSavedOrders(id);
            return oList;
        }

        List<Order> IItCommerceRepository.OrdersNotCompleted(int id)
        {
            List<Order> oList = OfficeStock.getListNotCompletedOrders(id);
            return oList;
        }

        List<Order> IItCommerceRepository.SearchOrders(PeriodParam obj)
        {
            List<Order> oList = OfficeStock.getListOrders(obj);
            return oList;
        }

        SavedOrder IItCommerceRepository.DeleteSavedOrder(int id)
        {
            SavedOrder result = OfficeStock.deleteSavedOrder(id);
            return result;
        }
        #endregion orders

        #region stock zone
        List<ProductInStock> IItCommerceRepository.SearchStock(int id, string name, string code)
        {
            List<ProductInStock> oList = OfficeStock.searchStockView(id, name, code);
            return oList;
        }//fin SearchStock

        List<ProductInStock> IItCommerceRepository.StockView(int id)
        {
            List<ProductInStock> oList = OfficeStock.getStockView(id);
            return oList;
        }//fin StockView

        List<ProductInStock> IItCommerceRepository.StockViewForSale(int id)
        {
            List<ProductInStock> oList = OfficeStock.getStockViewForSale(id);
            return oList;
        }//fin StockViewForSale

        List<ProductInStock> IItCommerceRepository.generateBarcodes(List<ProductInStock> list, int idProfil)
        {
            List<ProductInStock> oList = OfficeStock.generateBarcodes(list, idProfil);
            return oList;
        }//fin StockView

        List<ProductInStock> IItCommerceRepository.StockViewForSaleLimitByName(int id, string name)
        {
            List<ProductInStock> oList = OfficeStock.getStockViewForSaleLimitByName(id, name);
            return oList;
        }//fin StockViewForSale

        List<ProductInStock> IItCommerceRepository.StockViewForSaleLimitByCode(int id, string code)
        {
            List<ProductInStock> oList = OfficeStock.getStockViewForSaleLimitByCode(id, code);
            return oList;
        }//fin StockViewForSale


        ProductInStock IItCommerceRepository.UpdateStockView(ProductInStock obj, int id)
        {
            ProductInStock o = OfficeStock.updateStockView(obj, id);
            return o;
        }

        ProductInStock IItCommerceRepository.ExpandStockView(ProductInStock obj, int id)
        {
            ProductInStock o = OfficeStock.expandStockView(obj, id);
            return o;
        }

        List<ProductInStock> IItCommerceRepository.ProductStockView(PrmProdStkView ppsV)
        {
            List<ProductInStock> oList = OfficeStock.getProductStockView(ppsV.id_profil, ppsV.id_product);
            return oList;
        }
        #endregion stock zone

        List<ProductInStock> IItCommerceRepository.StockExpired(PeriodParam obj, int id)
        {
            List<ProductInStock> oList = OfficeStock.getStockExpired(obj, id);
            return oList;
        }

        // FileObject IItCommerceRepository.PrintBarcodes(PeriodParam param, string destinationPath, string resourcePath)
        // {
        //     List<ProductInStock> products = OfficeStock.generateBarcodes(param.products, param.agent.id);
        //     string filename = PrinterManager.printBarCodeList(products, destinationPath, resourcePath, param.agent.agency.company);
        //     FileObject newFile = new FileObject(filename);
        //     return newFile;
        // }

        #region sales
        // string IItCommerceRepository.CreateSale(Sale item, string destinationPath, string resourcePath)
        // {
        //     Sale result = OfficeStock.createSale(item);
        //     item.id = result.id;
        //     string saleReport = PrinterManager.printInvoice(item, destinationPath, resourcePath, item.agent.agency.company, ConfigObject.InvoiceType);
        //     return saleReport;
        // }

        //sell a product
        SaleDataView IItCommerceRepository.CreateSale(ProductSale _sale)
        {
            var oList = OfficeStock.createSale(_sale);
            return oList;
        }//fin CreateSale


        ProductSale IItCommerceRepository.PaySaleBalance(ProductSale item)
        {
            ProductSale result = OfficeStock.paySaleBalance(item);
            return result;
        }


        ProductSale IItCommerceRepository.CreateDevis(ProductSale item)
        {
            var oList = OfficeStock.createDevis(item);
            return oList;
           
        }

        List<ProductSale> IItCommerceRepository.Sales(int id)
        {
            List<ProductSale> oList = OfficeStock.getListSales(id);
            return oList;
        }
        List<ProductSale> IItCommerceRepository.SearchSales(PeriodParam obj)
        {
            List<ProductSale> oList = OfficeStock.getListSales(obj);
            return oList;
        }

        List<ProductSale> IItCommerceRepository.Devis(int id)
        {
            List<ProductSale> oList = OfficeStock.getListDevis(id);
            return oList;
        }
        List<ProductSale> IItCommerceRepository.SearchDevis(PeriodParam obj)
        {
            List<ProductSale> oList = OfficeStock.getListDevis(obj);
            return oList;
        }

        Invoice IItCommerceRepository.NormaliseInvoice(int id)
        {
            Invoice result = OfficeStock.normalizeSale(id);
            return result;
        }

        AvoirDataView IItCommerceRepository.CancelInvoice(ProductSale obj)
        {
            AvoirDataView result = OfficeStock.cancelSale(obj);
            return result;
        }
        #endregion sale

        #region services sales


        //sell a product
        async Task<byte[]> IItCommerceRepository.CreateServiceSale(ServiceSale _sale, ITemplateService _templateService, IConverter _converter)
        {
            byte[] oList = await OfficeStock.createServiceSale(_sale);
            return oList;
        }//fin CreateSale


        List<ServiceSale> IItCommerceRepository.ServiceSales(int id)
        {
            List<ServiceSale> oList = OfficeStock.getListServiceSales(id);
            return oList;
        }
        List<ServiceSale> IItCommerceRepository.SearchServiceSales(PeriodParam obj)
        {
            List<ServiceSale> oList = OfficeStock.getListServiceSales(obj);
            return oList;
        }


        #endregion services sales

        #region CryptagePassword
        string IItCommerceRepository.CryptWord(string password)
        {
            string Result = OfficeParams.cryptUserPassword(password);
            return Result;
        }
        #endregion CryptagePassword

        #region users
        List<User> IItCommerceRepository.Agents(int idProfil)
        {
            List<User> oList = OfficeAuth.getListAgents(idProfil);
            return oList;
        }

        List<User> IItCommerceRepository.Customers(int idProfil)
        {
            List<User> oList = OfficeAuth.getListCustomers(idProfil);
            return oList;
        }

        User IItCommerceRepository.CreateAgent(User item)
        {
            User result = OfficeAuth.createAgent(item);
            return result;
        }
        User IItCommerceRepository.UpdateAgent(User item)
        {
            User result = OfficeAuth.updateAgent(item);
            return result;
        }
        User IItCommerceRepository.DeleteAgent(int id)
        {
            User result = OfficeAuth.deleteAgent(id);
            return result;
        }

        utilisateur IItCommerceRepository.SearchUserWithEmail(string email)
        {
            utilisateur result = OfficeAuth.searchUserWithEmail(email);
            return result;
        }
        //*fin users*//
        #endregion users

        #region profiles
        List<Profil> IItCommerceRepository.Profiles(int id)
        {
            List<Profil> oList = OfficeAuth.getListProfils(id);
            return oList;
        }
        Profil IItCommerceRepository.CreateProfile(Profil item)
        {
            Profil result = OfficeAuth.createProfil(item);
            return result;
        }
        Profil IItCommerceRepository.UpdateProfile(Profil item)
        {
            Profil result = OfficeAuth.updateProfil(item);
            return result;
        }

        Profil IItCommerceRepository.UpdateCompanyLogo(string login, string logo)
        {
            Profil result = OfficeAuth.updateCompanyLogo(login, logo);

            return result;
        }

        Profil IItCommerceRepository.UpdateUserProfile(Profil item)
        {
            Profil result = OfficeAuth.updateUserProfil(item);
            return result;
        }

        Company IItCommerceRepository.UpdateCompanyProfile(Company item)
        {
            Company result = OfficeAuth.updateCompanyProfil(item);
            return result;
        }

        Profil IItCommerceRepository.DeleteProfile(int id)
        {
            Profil result = OfficeAuth.deleteProfil(id);
            return result;
        }

        Profil IItCommerceRepository.Signin(Profil item)
        {
            Profil result = OfficeAuth.signin(item);
            return result;
        }

        utilisateur IItCommerceRepository.Verified(string key)
        {
            utilisateur result = OfficeAuth.verifiedat(key);
            return result;
        } //

        Licence IItCommerceRepository.SaveLicence(Licence item)
        {
            Licence result = OfficeAuth.saveLicence(item);
            return result;
        } //

        Licence IItCommerceRepository.UpdateLicence(Licence item)
        {
            Licence result = OfficeAuth.updateLicence(item);
            return result;
        } //

        string IItCommerceRepository.GetCurrentLicenceCode()
        {
            Licence result = OfficeAuth.getLastLicence();
            if (result == null) return string.Empty;
            return result.code;
        }

        Licence IItCommerceRepository.GetCurrentLicence()
        {
            Licence result = OfficeAuth.getLastLicence();
            result.key = "";
            return result;
        }

        Profil IItCommerceRepository.GetActiveProfileByCode(string code)
        {
            Profil result = OfficeAuth.getActiveProfile(code);
            return result;
        }

        //*fin profiles*//
        #endregion profiles


        #region stock limit
        List<ProdStockLimit> IItCommerceRepository.StockLimit(int id)
        {
            List<ProdStockLimit> oList = OfficeStock.getStockLimit(id);
            return oList;
        }
        ProdStockLimit IItCommerceRepository.CreateStockLimit(ProdStockLimit item)
        {
            ProdStockLimit result = OfficeStock.createStockLimit(item);
            return result;
        }
        ProdStockLimit IItCommerceRepository.UpdateStockLimit(ProdStockLimit item)
        {
            ProdStockLimit result = OfficeStock.updateStockLimit(item);
            return result;
        }
        ProdStockLimit IItCommerceRepository.DeleteStockLimit(int id)
        {
            ProdStockLimit result = OfficeStock.deleteStockLimit(id);
            return result;
        }

        List<ProductInStock> IItCommerceRepository.GetProdOnAlertList(int id)
        {
            List<ProductInStock> result = OfficeStock.getListProdUnderStock(id);
            return result;
        }

        //*fin stock limit*//
        #endregion stock limit

    

        #region transfer
        StockTransfer IItCommerceRepository.CreateTransfer(StockTransfer item)
        {
            StockTransfer result = OfficeStock.createTransfer(item);
            return result;
        }
        List<StockTransfer> IItCommerceRepository.StockTransfers(int id)
        {
            List<StockTransfer> oList = OfficeStock.getListStockTransfers(id);
            return oList;
        }
        List<StockTransfer> IItCommerceRepository.SearchStockTransfers(PeriodParam obj)
        {
            List<StockTransfer> oList = OfficeStock.getListStockTransfers(obj);
            return oList;
        }

        #endregion transfer

        #region reports

        // FileObject IItCommerceRepository.PrintOrders(PeriodParam obj, string destinationPath, string resourcePath)
        // {
        //     obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);//because the sender is using gmt
        //     List<Order> gList = OfficeStock.getListOrders(obj);
        //     string pdfFile = PrinterManager.printOrderList(gList, obj.startDate, obj.endDate, destinationPath, resourcePath, obj.agent.agency.company);
        //     FileObject fileObj = new FileObject(pdfFile);
        //     return fileObj;
        // }

        Order IItCommerceRepository.PrintOrder(Order _order)
        {
            var oList = OfficeReporting.printOrderDetail(_order);
            return oList;
        }
     
        
        TransfertViewModel IItCommerceRepository.PrintTransfers(PeriodParam obj)
        {
            obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);//because the sender is using gmt
            List<StockTransfer> products = OfficeStock.getListStockTransfers(obj);
            var oList = OfficeReporting.printTransfertList(products, obj);
            return oList;
        }

        StockTransfer IItCommerceRepository.PrintTransfer(StockTransfer _transfert)
        {
            var oList = OfficeReporting.printTransfertDetail(_transfert);
            return oList;
        }



        SaleDetailView IItCommerceRepository.PrintDevis(ProductSale _sale)
        {
            SaleDetailView SaleDetailView = new SaleDetailView();
            SaleDataView oList = OfficeReporting.printSaleDetail(_sale);
            SaleDetailView.saleDetail = oList.saleData;
            return SaleDetailView;
        }




        #endregion report pdf


        #region stats

        GlobalReport IItCommerceRepository.GetCurrentReport(Profil obj)
        {
            GlobalReport result = OfficeStats.GetCurrentReport(obj);
            return result;
        }

        GlobalReport IItCommerceRepository.GetPeriodGlobalReport(PeriodParam obj)
        {
            GlobalReport result = OfficeStats.GetPeriodGlobalReport(obj);
            return result;
        }

        GlobalReport IItCommerceRepository.GetProductPeriodGlobalReport(PeriodParam obj)
        {
            GlobalReport result = OfficeStats.GetProductPeriodGlobalReport(obj);
            return result;
        }

        List<ProductInStock> IItCommerceRepository.GetProductMostSoldByQtyReport(PeriodParam obj)
        {
            List<ProductInStock> result = OfficeStats.GetMostSoldProdByQuantityReport(obj);
            return result;
        }
        List<ProductInStock> IItCommerceRepository.GetProductMostSoldByCaReport(PeriodParam obj)
        {
            List<ProductInStock> result = OfficeStats.GetMostSoldProdByCaReport(obj);
            return result;
        }
        List<ProductInStock> IItCommerceRepository.GetProductMostSoldByProfitReport(PeriodParam obj)
        {
            List<ProductInStock> result = OfficeStats.GetMostSoldProdByProfitReport(obj);
            return result;
        }

        List<UserReport> IItCommerceRepository.GetSalesReportBySellers(PeriodParam obj)
        {
            List<UserReport> result = OfficeStats.GetSalesReportBySellers(obj);
            return result;
        }

        List<UserReport> IItCommerceRepository.GetSalesReportByCustomers(PeriodParam obj)
        {
            List<UserReport> result = OfficeStats.GetSalesReportByCustomers(obj);
            return result;
        }

        #endregion stats

        #region saletarget

        List<SaleTarget> IItCommerceRepository.SaleTargetList(int id)
        {
            List<SaleTarget> oList = OfficeParams.getListSaleTarget(id);
            return oList;
        }

        List<SaleTarget> IItCommerceRepository.SaleTargetList(PeriodParam obj)
        {
            List<SaleTarget> oList = OfficeParams.getListSaleTarget(obj);
            return oList;
        }

        SaleTarget IItCommerceRepository.CreateSaleTarget(SaleTarget obj)
        {
            SaleTarget result = OfficeParams.createSaleTarget(obj);
            return result;
        }

        SaleTarget IItCommerceRepository.UpdateSaleTarget(SaleTarget obj)
        {
            SaleTarget result = OfficeParams.updateSaleTarget(obj);
            return result;
        }

        SaleTarget IItCommerceRepository.DeleteSaleTarget(int id)
        {
            SaleTarget result = OfficeParams.deleteSaleTarget(id);
            return result;
        }

        #endregion

        #region bank
        List<Bank> IItCommerceRepository.Banks(int id)
        {
            List<Bank> oList = OfficeParams.getListBanks(id);
            return oList;
        }

        //*bank*//
        Bank IItCommerceRepository.CreateBank(Bank bank)
        {
            Bank result = OfficeParams.createBank(bank);
            return result;
        }
        Bank IItCommerceRepository.UpdateBank(Bank bank)
        {
            Bank result = OfficeParams.updateBank(bank);
            return result;
        }
        Bank IItCommerceRepository.DeleteBank(int id)
        {
            Bank result = OfficeParams.deleteBank(id);
            return result;
        }

        #endregion bank

        #region virtual account
        List<AccountType> IItCommerceRepository.AccountTypes(int idProfil)
        {
            List<AccountType> oList = OfficeParams.getListAccountTypes(idProfil);
            return oList;
        }

        List<VirtualAccount> IItCommerceRepository.VirtualAccounts(int id)
        {
            List<VirtualAccount> oList = OfficeFinances.getListVirtualAccounts(id);
            return oList;
        }

        VirtualAccount IItCommerceRepository.CreateVirtualAccount(VirtualAccount bank)
        {
            VirtualAccount result = OfficeFinances.createVirtualAccount(bank);
            return result;
        }

        VirtualAccount IItCommerceRepository.UpdateVirtualAccount(VirtualAccount bank)
        {
            VirtualAccount result = OfficeFinances.updateVirtualAccount(bank);
            return result;
        }

        VirtualAccount IItCommerceRepository.DeleteVirtualAccount(int id)
        {
            VirtualAccount result = OfficeFinances.deleteVirtualAccount(id);
            return result;
        }

        #endregion virtual account


        #region fin operations
        List<FinOperation> IItCommerceRepository.FinOperations(int id)
        {
            List<FinOperation> oList = OfficeFinances.getListFinOperations(id);
            return oList;
        }

        List<FinOperation> IItCommerceRepository.SearchFinOperations(PeriodParam obj)
        {
            List<FinOperation> oList = OfficeFinances.getListFinOperations(obj);
            return oList;
        }

        FinOperation IItCommerceRepository.CreateFinOperation(FinOperation finOp)
        {
            FinOperation result = OfficeFinances.createFinOperation(finOp);
            return result;
        }
        #endregion

        #region logs
        List<LogItem> IItCommerceRepository.Logs(int id)
        {
            List<LogItem> oList = OfficeParams.getListLogs(id);
            return oList;
        }

        List<LogItem> IItCommerceRepository.Logs(PeriodParam obj)
        {
            List<LogItem> oList = OfficeParams.getListLogs(obj);
            return oList;
        }

        List<string> IItCommerceRepository.ErrorLogs()
        {
            List<string> oList = OfficeParams.getListErrors();
            return oList;
        }

        #endregion


        #region print documents


        //print an order
        Order IItCommerceRepository.PrintOrderDetail(Order _order)
        {
            var oList =  OfficeReporting.printOrderDetail(_order);
            return oList;
        }//end print order

        //print sale_invoice
        SaleDetailView IItCommerceRepository.PrintSaleDetail(ProductSale _sale)
        {
            SaleDetailView SaleDetailView = new SaleDetailView();
            SaleDataView oList = OfficeReporting.printSaleDetail(_sale);
            SaleDetailView.saleDetail = oList.saleData;
            SaleDetailView.invoice = oList.invoice;
            return SaleDetailView;
        }//end print sale_invoice

        //print avoir_invoice
        AvoirDetailView IItCommerceRepository.PrintAvoirDetail(ProductSale _sale)
        {
            AvoirDetailView SaleDetailView = new AvoirDetailView();
            AvoirDataView oList = OfficeReporting.printAvoirDetail(_sale);
            SaleDetailView.saleDetail = oList.saleData;
            SaleDetailView.invoice = oList.invoice;
            SaleDetailView.Ref_ORIGIN = oList.Ref_Origin ;
            SaleDetailView.Mecef_ORIGIN = oList.Mecef_Origin;
            return SaleDetailView;
        }//end print avoir_invoice 

        //print a sale list
        SaleViewModel IItCommerceRepository.PrintSaleList(PeriodParam obj)
        {
            obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);//because the sender is using gmt
            List<ProductSale> gList = OfficeStock.getListSales(obj);
            var oList = OfficeReporting.printSaleList(gList, obj);
            return oList;
        }//end print sale

        //print order list
        OrderViewModel IItCommerceRepository.PrintOrderList(PeriodParam obj)
        {
            obj.startDate = obj.startDate.AddHours(1); obj.endDate = obj.endDate.AddHours(1);//because the sender is using gmt
            List<Order> gList = OfficeStock.getListOrders(obj);
            var oList = OfficeReporting.printOrderList(gList, obj);
            return oList;
        }//end print orderlist

        //print PrintStockView
       StockViewModel IItCommerceRepository.PrintStockView(Profil obj)
        {
            List<ProductInStock> gList = OfficeStock.getStockView(obj.id);
            var oList = OfficeReporting.printStockView(gList);
            return oList;
        }//end print PrintStockView

        //print barcodes

        List<BarcodeViewModel> IItCommerceRepository.PrintBarcodes(PeriodParam obj)
        {
            List<ProductInStock> products = OfficeStock.printBarcodes(obj.products, obj.agent.id);
            var oList = OfficeReporting.printBarCodeList(products);
            return oList;
        }//end print PrintBarcodes  //update today

        #endregion

        #region paramétrage Mecef
        List<ParamMecef> IItCommerceRepository.ParamsMecef(int id)
        {
            var oList = OfficeParams.loadParamsMecef();
            return oList;
        }

        UpdateMecef IItCommerceRepository.UpdateParamMecef(UpdateMecef item)
        {
            UpdateMecef result = OfficeParams.updateParamMecef(item);
            return result;
        }
        #endregion paramétrage Mecef

        #region service de Jwrbearer
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ItCommerceRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetMyName()
        {
            var result = string.Empty;

            if (_httpContextAccessor.HttpContext != null)
            {

                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

            }
            return result;
        }
        #endregion service de Jwrbearer

        #region licence concerns

        
             
       

        ///get modules list
        List<ModuleRequest> IItCommerceRepository.GetModuleList()
        {
             List<ModuleRequest> _listModules = OfficeParams.GetModuleList();
            return _listModules;
        }//end GetModuleList

        ///get formules list
        List<FormuleRequest> IItCommerceRepository.GetFormuleList()
        {
              List<FormuleRequest> _listFormules = OfficeParams.GetFormuleList();
            return _listFormules;
        }//end GetFormuleList


        #endregion
    }
}
