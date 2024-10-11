using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QKPRobot
{
    internal class Robot
    {
        private string ipAddress = "";
        private bool useProxy = false;
        public bool IsMoving {

            get
            {
                int state = 1;
                   XArmAPI.get_state(ref state);
                if(state == 1)
                { return true; }
                else { return false; }
            
            }

        }
        // Read-only property of type Position
        public RobotPosition Position {

            get
            {
                float[] pose = { 0, 0, 0, 0, 0, 0 };
                XArmAPI.get_position(pose);
                return new RobotPosition(pose[0], pose[1], pose[2], pose[3], pose[4], pose[5]);
            }
        
        }

        public bool UseProxy
        {
            set
            {
                useProxy = value;
                XArmAPI.set_simulation_robot(useProxy);
            }
            get
            {
                return useProxy;
            }
        }


        public Robot(string IP_Address)
        {
            ipAddress = IP_Address;
            InitializeArm();

        }

        public void InitializeArm()
        {
            int ret;
            int arm1 = XArmAPI.create_instance(ipAddress, false);
            Console.WriteLine("create_instance: {0}", arm1);
            // int arm2 = XArmAPI.create_instance("192.168.1.135", false);
            // Console.WriteLine("create_instance: {0}", arm2);

            float[] pose1 = { 300, 0, 200, 180, 0, 0 };

            ret = XArmAPI.switch_xarm(arm1);
            Console.WriteLine("switch_xarm: {0}", ret);
            ret = XArmAPI.clean_warn();
            Console.WriteLine("clean_warn: {0}", ret);
            ret = XArmAPI.clean_error();
            Console.WriteLine("clean_error: {0}", ret);
            ret = XArmAPI.motion_enable(true);
            Console.WriteLine("motion_enable: {0}", ret);
            ret = XArmAPI.set_mode(0);
            Console.WriteLine("set_mode: {0}", ret);
            ret = XArmAPI.set_state(0);
            Console.WriteLine("set_state: {0}", ret);
        }

        public void Stop()
        {
            Console.WriteLine("Stopping");
            XArmAPI.set_mode(5); //set to cartesian velocity control mode
            XArmAPI.set_state(0);
            float[] stop = { 0, 0, 0, 0, 0, 0 };
            XArmAPI.vc_set_cartesian_velocity(stop, false, -1);
        }



       

        public void RockChop()
        {


            float[] tcpOffset = { 0, 0, 96, 0, 0, 0 };
            XArmAPI.set_tcp_offset(tcpOffset, false);
            XArmAPI.set_collision_sensitivity(1);
            XArmAPI.set_reduced_max_tcp_speed(100);
            XArmAPI.set_reduced_mode(true);

            //float[] pos0 = { -116, 411, 155, 174, 0, 2 };

            XArmAPI.set_mode(0);
            XArmAPI.set_state(0);


            float[] pos1 = { -116.4f, 411.0f, 155.4f, 174.7f, 0.0f, 2.7f };
            float[] pos2 = { -116.4f, 411.0f, 116.5f, 163.5f, 0.5f, 2.6f };
            float[] pos3 = { -116.4f, 411.0f, 99.1f, 171.0f, 0.2f, 2.6f };
            float[] pos4 = { -116.4f, 411.0f, 92.8f, 178.3f, -0.2f, 2.6f };
            float[] pos5 = { -116.4f, 411.0f, 155.4f, 174.7f, 0.0f, 2.7f };

            
            XArmAPI.set_position(pos1, 0, true, -1, false);
            XArmAPI.set_position(pos2, 10, true, -1, false);
            XArmAPI.set_position(pos3, 10, true, -1, false);
            XArmAPI.set_position(pos4, 10, true, -1, false);
            WaitForMotionEnd();
            XArmAPI.set_position(pos5, 20, true, -1, false);

            WaitForMotionEnd();

        }

        public void WaitForMotionEnd()
        {
            while (this.IsMoving)
            {
                Thread.Sleep(25);
            }
        }

        

        public void Disable()
        {
            int ret = XArmAPI.motion_enable(false);
        }

        public void Enable()
        {
            int ret = XArmAPI.motion_enable(true);
        }

        public void MoveZ(float speed)
        {
            XArmAPI.set_mode(5); //set to cartesian velocity control mode
            XArmAPI.set_state(0);
            XArmAPI.set_reduced_mode(false);
            float[] zDown = { 0, 0, speed, 0, 0, 0 };
            XArmAPI.vc_set_cartesian_velocity(zDown, false, -1);
        }

        public void MoveToPosition(RobotPosition pos, bool wait = true)
        {
            XArmAPI.set_collision_sensitivity(1);
            XArmAPI.set_reduced_max_tcp_speed(100);
            XArmAPI.set_reduced_mode(true);

            XArmAPI.set_mode(0);
            XArmAPI.set_state(0);

            if (wait)
            {
                
                XArmAPI.set_position(pos.ToArray(), 0, true, -1, false);
                WaitForMotionEnd();
            }
        }
    }
}
