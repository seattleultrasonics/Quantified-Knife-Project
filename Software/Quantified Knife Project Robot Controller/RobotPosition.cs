using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QKPRobot
{
    internal class RobotPosition
    {

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Pitch { get; set; }
        public float Roll { get; set; }
        public float Yaw { get; set; }

        public RobotPosition(float x, float y, float z, float pitch, float roll, float yaw)
        {
            X = x;
            Y = y;
            Z = z;
            Pitch = pitch;
            Roll = roll;
            Yaw = yaw;
        }

        public override string ToString()
        {
            return $"Position - X: {X}, Y: {Y}, Z: {Z}, Pitch: {Pitch}, Roll: {Roll}, Yaw: {Yaw}";
        }

        public float[] ToArray()
        {
            return new float[] { X, Y, Z, Pitch, Roll, Yaw };
        }
    }
}
