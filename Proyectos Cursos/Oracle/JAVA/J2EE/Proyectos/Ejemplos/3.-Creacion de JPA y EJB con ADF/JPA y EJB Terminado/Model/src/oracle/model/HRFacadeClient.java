package oracle.model;

import java.util.Hashtable;
import java.util.List;

import javax.naming.CommunicationException;
import javax.naming.Context;
import javax.naming.InitialContext;
import javax.naming.NamingException;

public class HRFacadeClient {
    public static void main(String[] args) {
        try {
            final Context context = getInitialContext();
            HRFacade hRFacade = (HRFacade)context.lookup("HR_EJB_JPA-Model-HRFacade#oracle.model.HRFacade");
  /*          for (Departments departments : (List<Departments>)hRFacade.getDepartmentsFindAll()) {
                printDepartments(departments);
            }
            for (Employees employees : (List<Employees>)hRFacade.getEmployeesFindAll()) {
                printEmployees(employees);
            }
*/
            for (Employees employees :
                 (List<Employees>)hRFacade.getEmployeesFindByName("P%") /* FIXME: Pass parameters here */) {
                printEmployees(employees);
            }
        } catch (CommunicationException ex) {
            System.out.println(ex.getClass().getName());
            System.out.println(ex.getRootCause().getLocalizedMessage());
            System.out.println("\n*** A CommunicationException was raised.  This typically\n*** occurs when the target WebLogic server is not running.\n");
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

    private static Context getInitialContext() throws NamingException {
        Hashtable env = new Hashtable();
        // WebLogic Server 10.x connection details
        env.put( Context.INITIAL_CONTEXT_FACTORY, "weblogic.jndi.WLInitialContextFactory" );
        env.put(Context.PROVIDER_URL, "t3://127.0.0.1:7101");
        return new InitialContext( env );
    }
}
