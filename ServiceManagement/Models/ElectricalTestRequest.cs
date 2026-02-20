using Microsoft.VisualBasic;
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

        // ---------- SHAFT RUN OUT ----------
        public decimal? ShaftRunOutA { get; set; }
        public decimal? ShaftRunOutB { get; set; }
        public decimal? ShaftRunOutC { get; set; }
        public decimal? ShaftRunOutD { get; set; }
        public decimal? ShaftRunOutE { get; set; }

        // ---------- BEARING 1 ----------
        public decimal? BearingLocationID1AA { get; set; }
        public decimal? BearingLocationID1BB { get; set; }
        public decimal? BearingOD1AA { get; set; }
        public decimal? BearingOD1BB { get; set; }
        public decimal? Cleaance1AA { get; set; }
        public decimal? Cleaance1BB { get; set; }
        public decimal? BearingID1AA { get; set; }
        public decimal? BearingID1BB { get; set; }
        public decimal? ShaftOD1AA { get; set; }
        public decimal? ShaftOD1BB { get; set; }
        public decimal? Interference1AA { get; set; }
        public decimal? Interference1BB { get; set; }

        // ---------- BEARING 2 ----------
        public decimal? BearingLocationID2AA { get; set; }
        public decimal? BearingLocationID2BB { get; set; }
        public decimal? BearingOD2AA { get; set; }
        public decimal? BearingOD2BB { get; set; }
        public decimal? Cleaance2AA { get; set; }
        public decimal? Cleaance2BB { get; set; }
        public decimal? BearingID2AA { get; set; }
        public decimal? BearingID2BB { get; set; }
        public decimal? ShaftOD2AA { get; set; }
        public decimal? ShaftOD2BB { get; set; }
        public decimal? Interference2AA { get; set; }
        public decimal? Interference2BB { get; set; }

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
        public List<MonitoringEquipmentModel> MonitoringEquipment { get; set; }
    }



    public class MonitoringEquipmentModel
    {
        public string Sensors { get; set; }
        public string Terminal { get; set; }
        public decimal Values { get; set; }
        public string StandValues { get; set; }
        public string Notes { get; set; }
    }
}