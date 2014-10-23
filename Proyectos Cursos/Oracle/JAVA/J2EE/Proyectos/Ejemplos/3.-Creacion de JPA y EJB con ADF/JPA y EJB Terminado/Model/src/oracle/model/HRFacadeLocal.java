package oracle.model;

import java.math.BigDecimal;

import java.util.List;

import javax.ejb.Local;

@Local
public interface HRFacadeLocal {
    Object queryByRange(String jpqlStmt, int firstResult, int maxResults);

    Departments persistDepartments(Departments departments);

    Departments mergeDepartments(Departments departments);

    void removeDepartments(Departments departments);

    List<Departments> getDepartmentsFindAll();

    Employees persistEmployees(Employees employees);

    Employees mergeEmployees(Employees employees);

    void removeEmployees(Employees employees);

    List<Employees> getEmployeesFindAll();

    List<Employees> getEmployeesFindByName(String p_name);
}
