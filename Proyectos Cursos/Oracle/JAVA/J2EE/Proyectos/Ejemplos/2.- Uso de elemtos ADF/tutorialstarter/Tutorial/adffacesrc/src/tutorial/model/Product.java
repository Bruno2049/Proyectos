package tutorial.model;

import java.util.Date;

public class Product {
    public Product() {
    }
    
    String name;
    float cost;
    String category;
    Date manufactured;
    String image;
    int rating;
    String description;

    public Product(String name1, String category1, float cost1, 
                   Date manufactured1, String image1, int rating1, String description1) {
        super();
        this.name = name1;
        this.category = category1;
        this.cost = cost1;
        this.manufactured = manufactured1;
        this.image = image1;
        this.rating = rating1;
        this.description=description1;
    }

    public void setName(String newname) {
        this.name = newname;
    }

    public String getName() {
        return name;
    }

    public void setCategory(String newcategory) {
        this.category = newcategory;
    }

    public String getCategory() {
        return category;
    }

    public void setCost(float newcost) {
        this.cost = newcost;
    }

    public float getCost() {
        return cost;
    }

    public void setManufactured(Date newmanufactured) {
        this.manufactured = newmanufactured;
    }

    public Date getManufactured() {
        return manufactured;
    }

    public void setImage(String newimage) {
        this.image = newimage;
    }

    public String getImage() {
        return image;
    }

    public void setRating(int newrating) {
        this.rating = newrating;
    }

    public int getRating() {
        return rating;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getDescription() {
        return description;
    }
}
