using System;
using System.Collections.Generic;

namespace LD4_10_MKuliesius.AppCode
{
    /// <summary>
    /// container/register of a shop and all of its items
    /// </summary>
    public class DevicesContainer
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNum { get; set; }

        private List<Device> AllDevices;

        public DevicesContainer() 
        {
            AllDevices = new List<Device>();
        }   
        public DevicesContainer(string name, string address, string phoneNum, List<Device> devices)
        { 
            Name = name;
            Address = address;
            PhoneNum = phoneNum;
            AllDevices = new List<Device>(devices);
        }

        public string GetName() 
        {
            return Name; 
        }

        public void Add(Device device) 
        {
            AllDevices.Add(device);
        }

        public int ItemsCount() 
        {
            return this.AllDevices.Count;
        }

        public Device GetByIndex(int index) 
        {
            return this.AllDevices[index];
        }

        public List<Device> GetAll() 
        {
            return AllDevices;
        }
        public bool IsEmpty()
        {
            if (AllDevices.Count > 0) 
            {
                return false;
            }
            else return true;
        }

        public int Count()
        {
            return AllDevices.Count;
        }

        public Device Get(int index)
        {
            if (index >= 0 && index < AllDevices.Count)
            {
                return AllDevices[index];
            }
            throw new IndexOutOfRangeException("Index is out of range for device list.");
        }
    }
}