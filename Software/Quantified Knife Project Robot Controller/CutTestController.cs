using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QKPRobot
{
    internal class CutTestController
    {
        private const double zMinimumForceThreshold = .5;
        private Robot robot;
        private RuishanScale scale;
        public CutTestDataSet dataSet;
        public double ZMinimum = -1;
        public RobotPosition EndPosition;
        public bool TestRunning = false;

        public string BladeID, Sample = "";
        public int Trial;
        public Stopwatch TimeElapsed = new Stopwatch();
        public double PosX, PosY, PosZ, Pitch, Roll, Yaw, Force;

        private float zMinimumMargin = 1; //mm space above the cutting deck
        public float[] TestStartingJointPose = { 9.8f, -13.6f, -57.4f, 0f, 104.5f, 45f }; //joint
        public float[] TestEndingJointPoseForZ= { 10.1f, -13.3f, -37.4f, -4f, 65.6f, 47.9f };
        //float[] TestEndingJointPoseForZ = { 10f, -8.2f, -31.3f, -5.6f, 51.5f, 49.7f };
        public float[] PushCutStartingPose = { 319.9f, 45f, 190, 166.2f, 7.2f, -35.8f }; //XYZ


        int dataCalls = 0;

        //public CutTestController(Robot r, RuishanScale s, CutTestDataSet ds) { 
        public CutTestController(Robot r, RuishanScale s, CutTestDataSet ds)
        {
            robot = r;
            scale = s;
            dataSet = ds;

        }

        public void SetTrialInfo(string bladeID, string sample, int trial)
        {
            BladeID = bladeID;
            Sample = sample;   
            Trial = trial;  
        }

        public double FindZMin()
        {
            //GoToSliceEndAngleforZ();
            /*
            robot.MoveZ(-3);
            while (scale.Mass <= zMinimumForceThreshold)
            {
                Console.WriteLine(robot.Position.ToString());
                Thread.Sleep(10);
            }
            robot.Stop();
            Console.WriteLine("Bottomed! Retracting");
            RobotPosition retract = robot.Position;
            retract.Z += 16;

            robot.MoveToPosition(retract, true); 

            while(robot.IsMoving)
                {
                Thread.Sleep(50);
                }
            
            Console.WriteLine("Slow Homing");
            Thread.Sleep(1000);
            */
            robot.MoveZ(-1.0f);
            
            while (scale.Mass <= zMinimumForceThreshold)
            {
                Console.WriteLine(robot.Position.ToString());
                Thread.Sleep(10);
            }
            robot.Stop();
            ZMinimum = robot.Position.Z + zMinimumMargin;
            EndPosition = robot.Position;
            EndPosition.Z += zMinimumMargin;
            Console.WriteLine("Z End = " + ZMinimum);

            //make the starting pose automatically 50mm above the found Z
            PushCutStartingPose[2] = (float)ZMinimum + 50;

            //XArmAPI.set_position(TestStartingJointPose, false, -1, false);
            GoToStart();

            return ZMinimum;
        }

        public void PushCut()
        {
            GoToPushCutStart();

            RobotPosition PushCutStart = robot.Position;
            //PushCutStart.Z += 100;
            
            RobotPosition PushCutEnd = PushCutStart;
            PushCutEnd.Z = (float) ZMinimum;
            float[] endPosition = PushCutEnd.ToArray();
            
            
            //XArmAPI.set_reduced_max_tcp_speed(9f);
            //XArmAPI.set_reduced_mode(true);
            XArmAPI.set_collision_sensitivity(1, true);

            
            TimeElapsed.Reset();
            TimeElapsed.Start();
            TestRunning = true;
            XArmAPI.set_reduced_max_tcp_speed(4f);
            XArmAPI.set_reduced_mode(true);
            XArmAPI.set_mode(0);
            XArmAPI.set_state(0);
            XArmAPI.set_position(endPosition, true, -1, false);
            TestRunning = false;

            GoToPushCutStart();
            XArmAPI.set_collision_sensitivity(3, true);

        }

        /*
        public void PushSlice()
        {
            GoToPushCutStart();

            
            XArmAPI.set_mode(0);
            XArmAPI.set_state(0);
            XArmAPI.set_reduced_max_tcp_speed(8);
            XArmAPI.set_reduced_mode(true);
            XArmAPI.set_collision_sensitivity(1, true);

            TestRunning = true;
            GoToPushCutStart();
            TimeElapsed.Reset();

            TimeElapsed.Start();

            float[] pose2 = { 320f, 45.2f, (float)ZMinimum, 166.3f, 7.2f, -35.8f };
            XArmAPI.set_position(pose2, true, -1, false);

            TestRunning = false;
            TimeElapsed.Stop();

            GoToPushCutStart();
            XArmAPI.set_collision_sensitivity(3, true);
            
        }
        */

        public void GoToPushCutStart()
        {
            XArmAPI.set_mode(0);
            XArmAPI.set_state(0);
            XArmAPI.set_reduced_max_tcp_speed(50f);
            XArmAPI.set_position(PushCutStartingPose, true, -1, false);

            XArmAPI.set_reduced_max_tcp_speed(9f);
            XArmAPI.set_mode(0);
            XArmAPI.set_state(0);

        }

        public void Slice()
        {
            dataCalls = 0;
            if (EndPosition == null)
            {
                DialogResult result = MessageBox.Show("No end position set. Use last known end position?", "No Z-Min Set", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    RobotPosition p = robot.Position;
                    p.Z = (float)ZMinimum;
                    EndPosition = p;
                }
                else
                {
                    return;
                }
            }

            float[] jpose1 = TestStartingJointPose;
            float[] pose1 = { 303.0f, 45.1f, 234.3f, 160.6f, 14.7f, -37.2f };
            float[] pose2 = { 320f, 45.2f, (float)ZMinimum, 166.3f, 7.2f, -35.8f };
            float[] poseFinish = { 0, 0, 50, 0, 0, 0 };
            //XArmAPI.set_collision_sensitivity(0, true);
            XArmAPI.set_collision_sensitivity(0, true);
            XArmAPI.set_mode(0);
            XArmAPI.set_state(0);
            
            //XArmAPI.set_collision_sensitivity(3);
            XArmAPI.set_reduced_max_tcp_speed(9f);
            XArmAPI.set_joint_maxacc(200);
            XArmAPI.set_tcp_maxacc(250);
            //XArmAPI.set_reduced_max_joint_speed(10);
            XArmAPI.set_reduced_mode(true);
            

            XArmAPI.set_servo_angle(jpose1, 10, 200, 0, true);
            TimeElapsed.Reset();
            TestRunning = true;
            TimeElapsed.Start();
            XArmAPI.set_position(pose1, false, -1, false);
            XArmAPI.set_position(pose2, true, -1, false);

            TestRunning = false;
            XArmAPI.set_collision_sensitivity(3, true);
            GoToStart();
            //XArmAPI.set_position(poseFinish, false, -1, true); //relative move to finish pose
            Console.WriteLine("Data calls: " + dataCalls);

        }

        public void GoToSliceEndAngleforZ()
        {
            float[] pose = TestEndingJointPoseForZ;
            XArmAPI.set_mode(0);
            XArmAPI.set_state(0);

            //XArmAPI.set_collision_sensitivity(3);
            //XArmAPI.set_reduced_max_tcp_speed(35);
            //XArmAPI.set_joint_maxacc(200);
            //XArmAPI.set_tcp_maxacc(500);
            //XArmAPI.set_reduced_max_joint_speed(10);
            //XArmAPI.set_reduced_mode(true);

            XArmAPI.set_servo_angle(pose, 10, 200, 0, true);
        }

        public void GoToStart()
        {
            float[] pose = TestStartingJointPose;
            XArmAPI.set_mode(0);
            XArmAPI.set_state(0);
            //XArmAPI.set_reduced_mode(false);
            XArmAPI.set_reduced_max_joint_speed(900);
            //XArmAPI.set_joint_maxacc(1000);
            XArmAPI.set_servo_angle(pose, 10, 200, 0, false);
            //XArmAPI.set_reduced_mode(true);
        }

        public DataRow GetCurrentData()
        {
            DataRow newRow = dataSet.CutTestRawData.NewRow();
            newRow["BladeID"] = BladeID;
            newRow["Sample"] = Sample;
            newRow["Trial"] = Trial;
            RobotPosition rp = robot.Position;
            newRow["PosX"] = rp.X;
            newRow["PosY"] = rp.Y;
            newRow["PosZ"] = rp.Z;
            newRow["Pitch"] = rp.Pitch;
            newRow["Roll"] = rp.Roll;
            newRow["Yaw"] = rp.Yaw;
            newRow["TimeElapsedms"] = TimeElapsed.ElapsedMilliseconds;
            newRow["Force"] = scale.Mass;
            newRow["SeriesName"] = BladeID+"-"+Sample+"-"+Trial;

            dataCalls++;
            return newRow;
        }

        public bool CheckScaleZeroed()
        {
            if (scale.Mass < -.1 || scale.Mass > .1)
            {
                DialogResult result = MessageBox.Show("Scale is not tared. Continue?", "Scale not at 0.0", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else return true;
            
        }

    }
}
