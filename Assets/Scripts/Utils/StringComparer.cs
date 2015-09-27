using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StringComparer : IEqualityComparer<string>
{
	public bool Equals(string x, string y)
	{
		//Check whether the compared objects reference the same data.
		if (Object.ReferenceEquals(x, y)) return true;
		
		//Check whether any of the compared objects is null.
		if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
			return false;
		
		//Check whether the products' properties are equal.
		return x.Equals(y);		
	}
	
	public int GetHashCode(string x)
	{
		//Check whether the object is null
		if (Object.ReferenceEquals(x, null)) return 0;
		
		//Get hash code for the Name field if it is not null.
		int hashProductName = x == null ? 0 : x.GetHashCode();
		
		//Get hash code for the Code field.
		int hashProductCode = x.GetHashCode();
		
		//Calculate the hash code for the product.
		return hashProductName ^ hashProductCode;		
	}
	
}

