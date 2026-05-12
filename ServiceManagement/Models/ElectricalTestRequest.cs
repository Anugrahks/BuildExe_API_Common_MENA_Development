using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuildExeServiceManagement.Models
{
    public class ElectricalTestRequest
    {
        // ---------- MASTER LINK ----------
        public int PumpMasterId { get; set; }

        // ---------- POWER CABLE 1 ----------
        public decimal? PowerCable1_BrownGray { get; set; }
        public decimal? PowerCable1_BrownE { get; set; }
        public decimal? PowerCable1_BrownBlk { get; set; }
        public decimal? PowerCable1_BlockE { get; set; }
        public decimal? PowerCable1_GrayBlk { get; set; }
        public decimal? PowerCable1_GrayE { get; set; }
        public string PowerCable1_Notes { get; set; }

        // ---------- POWER CABLE 2 ----------
        public decimal? PowerCable2_BrownGray { get; set; }
        public decimal? PowerCable2_BrownE { get; set; }
        public decimal? PowerCable2_BrownBlk { get; set; }
        public decimal? PowerCable2_BlockE { get; set; }
        public decimal? PowerCable2_GrayBlk { get; set; }
        public decimal? PowerCable2_GrayE { get; set; }
        public string PowerCable2_Notes { get; set; }

        // ---------- POWER CABLE 3 ----------
        public decimal? PowerCable3_BrownGray { get; set; }
        public decimal? PowerCable3_BrownE { get; set; }
        public decimal? PowerCable3_BrownBlk { get; set; }
        public decimal? PowerCable3_BlockE { get; set; }
        public decimal? PowerCable3_GrayBlk { get; set; }
        public decimal? PowerCable3_GrayE { get; set; }
        public string PowerCable3_Notes { get; set; }

        // ---------- POWER CABLE 4 ----------
        public decimal? PowerCable4_BrownGray { get; set; }
        public decimal? PowerCable4_BrownE { get; set; }
        public decimal? PowerCable4_BrownBlk { get; set; }
        public decimal? PowerCable4_BlockE { get; set; }
        public decimal? PowerCable4_GrayBlk { get; set; }
        public decimal? PowerCable4_GrayE { get; set; }
        public string PowerCable4_Notes { get; set; }

        // ---------- CONTROL CABLE ----------
        public string ControlCableInsulationResistanceNotes { get; set; }
        public string ControlCableContinuityTestNotes { get; set; }

        // ---------- MOTOR WINDING ----------
        public decimal? MotorWindingU1V1 { get; set; }
        public decimal? MotorWindingU1E { get; set; }
        public decimal? MotorWindingU1W1 { get; set; }
        public decimal? MotorWindingV1E { get; set; }
        public decimal? MotorWindingV1W1 { get; set; }
        public decimal? MotorWindingW1E { get; set; }
        public decimal? MotorWindingU1U2 { get; set; }
        public decimal? MotorWindingUV { get; set; }
        public decimal? MotorWindingV1V2 { get; set; }
        public decimal? MotorWindingUW { get; set; }
        public decimal? MotorWindingW1W2 { get; set; }
        public decimal? MotorWindingVW { get; set; }

        // ---------- NOTES ----------
        public string InsulationResistanceNotes { get; set; }
        public string WindingResistanceNotes { get; set; }
        public string MonitoringEquipmentNotes { get; set; }

       
        public string ShaftRunOutRemarks { get; set; }




        // ---------- PROPELLER ----------
        public decimal? BellMouthLinerIDAA { get; set; }
        public decimal? BellMouthLinerIDBB { get; set; }
        public decimal? PropellerODAA { get; set; }
        public decimal? PropellerODBB { get; set; }
        public decimal? RunningClearance1AA { get; set; }
        public decimal? RunningClearance1BB { get; set; }
        public decimal? PropellerBoreIdAA { get; set; }
        public decimal? PropellerBoreIdBB { get; set; }
        public decimal? ShaftOD3AA { get; set; }
        public decimal? ShaftOD3BB { get; set; }
        public decimal? RunningClearance2AA { get; set; }
        public decimal? RunningClearance2BB { get; set; }

        // ---------- CHILD ----------

        public List<ShaftRunOutEntry> ShaftRunOutValues { get; set; }
        public List<BearingSet> BearingSets { get; set; }
        public List<MonitoringEquipmentModel> MonitoringEquipment { get; set; }
    }

    public class ShaftRunOutEntry
    {
        public string FieldKey { get; set; }
        public decimal? FieldValue { get; set; }
    }

    public class BearingSet
    {
       
        public string Name { get; set; }
        public decimal BearingLocationIDAA { get; set; }
        public decimal BearingLocationIDBB { get; set; }
        public decimal BearingODAA { get; set; }
        public decimal BearingODBB { get; set; }
        public decimal CleaanceAA { get; set; }
        public decimal CleaanceBB { get; set; }
        public decimal BearingIDAA { get; set; }
        public decimal BearingIDBB { get; set; }
        public decimal ShaftODAA { get; set; }
        public decimal ShaftODBB { get; set; }
        public decimal InterferenceAA { get; set; }
        public decimal InterferenceBB { get; set; }
       
        public string Tolerance1 { get; set; }

       
        public string Tolerance2 { get; set; }
    }

    public class MonitoringEquipmentModel
    {
        [JsonProperty("Sensor")]
        public string Sensors { get; set; }
        public string Terminal { get; set; }
        public decimal Values { get; set; }
        public string StandValues { get; set; }
        public string Notes { get; set; }
    }
}