/**
 * @(#) Driver.java
 */

 import java.Time.LocalDate;



public class Driver extends RegisteredUser
{
	public Delivery krovinioPristatymas;
	
	public Route pristatyimas;
	
	//private CodeGenClasses.ModelList.Models.DriverStatus Busena;
	
	public Date isvykimoData;
	public String wifeName;
	public String truckName;
	
	
	public void GetDriverPreferences( )
	{
		
	}
	
	public void UpdateStatus( )
	{
		System.out.println("Updated");
	}
	
	public void UpdateDate( )
	{
		IsvykimoData = LocalDate.now();
	}
	
	public String returnWifeName()
	{
		return WifeName;
	}

	public LocalDate returnLeaveDate()
	{
		return IsvykimoData;
	}
	public String returnTruckName()
	{
		return truckName;
	}
	
}
