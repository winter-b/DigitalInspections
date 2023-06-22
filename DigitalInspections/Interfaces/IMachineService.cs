﻿using DigitalInspectionsWebApi.Models; using DigitalInspectionsWebApi.Models.Requests;  namespace DigitalInspectionsWebApi.Interfaces {     public interface IMachineService     {         Machine CreateMachine(MachineRequest machine);         void DeleteMachine(Guid machineId);         Machine GetMachine(Guid machineId);         List<Machine> GetMachines(int limit, int offset);         Machine UpdateMachine(MachineRequest machine, Guid machineId);     } } 