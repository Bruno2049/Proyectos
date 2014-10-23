package oracle.model;

import java.io.Serializable;

import java.math.BigDecimal;

import java.util.Date;

import java.util.List;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

@Entity
@NamedQueries( { @NamedQuery(name = "Employees.findAll", query = "select o from Employees o"),
                 @NamedQuery(name = "Employees.findBySal", query = "select o from Employees o where o.salary > :p_sal"),
                 @NamedQuery(name = "Employees.findByName",query = "select o from Employees o where o.firstName like :p_name")})
public class Employees implements Serializable {
    @Column(name = "COMMISSION_PCT")
    private BigDecimal commissionPct;
    @Column(nullable = false, unique = true, length = 25)
    private String email;
    @Id
    @Column(name = "EMPLOYEE_ID", nullable = false)
    private BigDecimal employeeId;
    @Column(name = "FIRST_NAME", length = 20)
    private String firstName;
    @Temporal(TemporalType.DATE)
    @Column(name = "HIRE_DATE", nullable = false)
    private Date hireDate;
    @Column(name = "JOB_ID", nullable = false, length = 10)
    private String jobId;
    @Column(name = "LAST_NAME", nullable = false, length = 25)
    private String lastName;
    @Column(name = "PHONE_NUMBER", length = 20)
    private String phoneNumber;
    private BigDecimal salary;
    @ManyToOne
    @JoinColumn(name = "MANAGER_ID")
    private Employees employees;
    @OneToMany(mappedBy = "employees")
    private List<Employees> employeesList;
    @OneToMany(mappedBy = "employees")
    private List<Departments> departmentsList;
    @ManyToOne
    @JoinColumn(name = "DEPARTMENT_ID")
    private Departments departments;

    public Employees() {
    }

    public Employees(BigDecimal commissionPct, Departments departments, String email, BigDecimal employeeId,
                     String firstName, Date hireDate, String jobId, String lastName, Employees employees,
                     String phoneNumber, BigDecimal salary) {
        this.commissionPct = commissionPct;
        this.departments = departments;
        this.email = email;
        this.employeeId = employeeId;
        this.firstName = firstName;
        this.hireDate = hireDate;
        this.jobId = jobId;
        this.lastName = lastName;
        this.employees = employees;
        this.phoneNumber = phoneNumber;
        this.salary = salary;
    }

    public BigDecimal getCommissionPct() {
        return commissionPct;
    }

    public void setCommissionPct(BigDecimal commissionPct) {
        this.commissionPct = commissionPct;
    }


    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public BigDecimal getEmployeeId() {
        return employeeId;
    }

    public void setEmployeeId(BigDecimal employeeId) {
        this.employeeId = employeeId;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public Date getHireDate() {
        return hireDate;
    }

    public void setHireDate(Date hireDate) {
        this.hireDate = hireDate;
    }

    public String getJobId() {
        return jobId;
    }

    public void setJobId(String jobId) {
        this.jobId = jobId;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }


    public String getPhoneNumber() {
        return phoneNumber;
    }

    public void setPhoneNumber(String phoneNumber) {
        this.phoneNumber = phoneNumber;
    }

    public BigDecimal getSalary() {
        return salary;
    }

    public void setSalary(BigDecimal salary) {
        this.salary = salary;
    }

    public Employees getEmployees() {
        return employees;
    }

    public void setEmployees(Employees employees) {
        this.employees = employees;
    }

    public List<Employees> getEmployeesList() {
        return employeesList;
    }

    public void setEmployeesList(List<Employees> employeesList) {
        this.employeesList = employeesList;
    }

    public Employees addEmployees(Employees employees) {
        getEmployeesList().add(employees);
        employees.setEmployees(this);
        return employees;
    }

    public Employees removeEmployees(Employees employees) {
        getEmployeesList().remove(employees);
        employees.setEmployees(null);
        return employees;
    }

    public List<Departments> getDepartmentsList() {
        return departmentsList;
    }

    public void setDepartmentsList(List<Departments> departmentsList) {
        this.departmentsList = departmentsList;
    }

    public Departments addDepartments(Departments departments) {
        getDepartmentsList().add(departments);
        departments.setEmployees(this);
        return departments;
    }

    public Departments removeDepartments(Departments departments) {
        getDepartmentsList().remove(departments);
        departments.setEmployees(null);
        return departments;
    }

    public Departments getDepartments() {
        return departments;
    }

    public void setDepartments(Departments departments) {
        this.departments = departments;
    }
}
