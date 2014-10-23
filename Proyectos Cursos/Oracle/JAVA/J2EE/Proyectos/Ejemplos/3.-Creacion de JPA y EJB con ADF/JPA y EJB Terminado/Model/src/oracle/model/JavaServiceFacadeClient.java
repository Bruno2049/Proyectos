package oracle.model;

import java.util.List;

public class JavaServiceFacadeClient {
    public static void main(String[] args) {
        try {
            final JavaServiceFacade javaServiceFacade = new JavaServiceFacade();
/*            for (Departments departments : (List<Departments>)javaServiceFacade.getDepartmentsFindAll()) {
                printDepartments(departments);
            }
            for (Employees employees : (List<Employees>)javaServiceFacade.getEmployeesFindAll()) {
                printEmployees(employees);
            }
*/
            for (Employees employees :
                 (List<Employees>)javaServiceFacade.getEmployeesFindByName("P%") /* FIXME: Pass parameters here */) {
                printEmployees(employees);
 //               Employees a = javaServiceFacade.getEmployeesFindByName("P%").get(0);
 //               System.out.println(a.getLastName());
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    private static void printDepartments(Departments departments) {
        System.out.println("departmentId = " + departments.getDepartmentId());
        System.out.println("departmentName = " + departments.getDepartmentName());
        System.out.println("locationId = " + departments.getLocationId());
        System.out.println("employees = " + departments.getEmployees());
        System.out.println("employeesList = " + departments.getEmployeesList());
    }

    private static void printEmployees(Employees employees) {
        System.out.println("commissionPct = " + employees.getCommissionPct());
        System.out.println("email = " + employees.getEmail());
        System.out.println("employeeId = " + employees.getEmployeeId());
        System.out.println("firstName = " + employees.getFirstName());
        System.out.println("hireDate = " + employees.getHireDate());
        System.out.println("jobId = " + employees.getJobId());
        System.out.println("lastName = " + employees.getLastName());
        System.out.println("phoneNumber = " + employees.getPhoneNumber());
        System.out.println("salary = " + employees.getSalary());
        System.out.println("employees = " + employees.getEmployees());
        System.out.println("employeesList = " + employees.getEmployeesList());
        System.out.println("departmentsList = " + employees.getDepartmentsList());
        System.out.println("departments = " + employees.getDepartments());
    }
}
