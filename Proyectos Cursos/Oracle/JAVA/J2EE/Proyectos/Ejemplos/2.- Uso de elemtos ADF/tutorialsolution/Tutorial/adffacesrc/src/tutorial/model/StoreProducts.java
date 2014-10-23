package tutorial.model;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;

import java.util.HashMap;
import java.util.Map;

import oracle.binding.AttributeContext;
import oracle.binding.DataControl;
import oracle.binding.ManagedDataControl;
import oracle.binding.OperationBinding;
import oracle.binding.RowContext;
import oracle.binding.TransactionalDataControl;
import oracle.binding.UpdateableDataControl;

public class StoreProducts implements TransactionalDataControl, UpdateableDataControl, ManagedDataControl {

    Collection<Product> products = new ArrayList<Product>();

    public StoreProducts() {
        //Some Products to start our store.
        Product a =
            new Product("iPod Nano", "Electronics", 150, new Date(), "/images/nano.gif", 8, "Instead of the hard disk which is used in the iPod Classic, the Nano uses flash memory. This means there are no moving parts related to memory, making the iPod Nano resistant to memory failure due to sudden movement.");
        products.add(a);
        a =
  new Product("iPod Classic", "Electronics", 250, new Date(), "/images/classic.gif", 7, "The iPod Classic is a portable media player marketed by Apple Inc. To date, there have been six generations of the iPod Classic, as well as a spin-off (the iPod Photo) that was later re-integrated into the main Classic line. All generations use a 1.8-inch hard drive for storage.");
        products.add(a);
        a =
  new Product("Macbook", "Computers", 1200, new Date(), "/images/mac.gif", 9, "MacBook is aimed at the education and consumer markets. It is the best-selling Macintosh in history, and according to the sales-research organization NPD Group, the midrange model of the MacBook has been the single best-selling laptop of any brand in U.S. retail stores for the past five months.");
        products.add(a);
        a =
  new Product("MacbookPro", "Computers", 1700, new Date(), "/images/macpro.gif", 7, "MacBook Pro replaced the PowerBook G4 and was the second computer to be announced in the Apple Intel transition (after the iMac). Positioned at the high end of the MacBook family, the MacBook Pro is aimed at the professional and power user market.");
        products.add(a);

    }

    public void addProduct(String pname, String pcategory, float pprice, Date pmanufactured, String pimage,
                           int prating, String description) {
        Product a = new Product(pname, pcategory, pprice, pmanufactured, pimage, prating, description);
        this.products.add(a);
    }

    public void setProducts(Collection<Product> newproducts) {
        this.products = newproducts;
    }

    public Collection<Product> getProducts() {
        return products;
    }

    public String getName() {
        return null;
    }

    public void release() {
    }

    public Object getDataProvider() {
        return null;
    }

    public boolean invokeOperation(Map p0, OperationBinding p1) {
        return false;
    }

    public boolean isTransactionDirty() {
        return false;
    }

    public void rollbackTransaction() {
    }

    public void commitTransaction() {
    }

    public boolean setAttributeValue(AttributeContext p0, Object p1) {
        return false;
    }

    public Object createRowData(RowContext p0) {
        return null;
    }

    public Object registerDataProvider(RowContext p0) {
        return null;
    }

    public boolean removeRowData(RowContext p0) {
        return false;
    }

    public void validate() {
    }

    public void beginRequest(HashMap p0) {
    }

    public void endRequest(HashMap p0) {
    }

    public boolean resetState() {
        return false;
    }
}
