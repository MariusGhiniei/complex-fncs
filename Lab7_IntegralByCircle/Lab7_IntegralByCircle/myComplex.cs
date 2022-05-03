using System.ComponentModel;
using System.Threading.Channels;

namespace Lab7_IntegralByCircle;

public class myComplex
{
    public double re, im;
    
    public myComplex(double x, double y)
    {
        this.re = x;
        this.im = y;
    }
    
    public double Re
    {
        get { return re; }
        set { re = value; }
    }

    public double Im
    {
        get { return im; }
        set { im = value; }
    }
    public double Ro
    {
        get { return Math.Sqrt(re * re + im * im); }
        set
        {
            re = value * Math.Cos(Theta);
            im = value * Math.Sin(Theta);
        }
    }
    public double Ro2
    {
        get { return re * re + im * im; }
    }
    
    public double Theta
    {
        get { return Math.Atan2(im, re); }
        set
        {
            re = Ro * Math.Cos(value);
            im = Ro * Math.Sin(value);
        }
    }
    
    public static myComplex setRoTheta(double Ro, double theta)
    {
        return new myComplex(Ro * Math.Cos(theta), Ro * Math.Sin(theta));
    }
    
    public static myComplex operator +(myComplex zst, myComplex zdr)
    {
        return new myComplex(zst.re + zdr.re, zst.im + zdr.im);
    }
    
    public static myComplex operator -(myComplex zst, myComplex zdr)
    {
        return new myComplex(zst.re - zdr.re, zst.im - zdr.im);
    }
    
    public static myComplex operator *(myComplex zst, myComplex zdr)
    {
        return new myComplex(zst.re * zdr.re - zst.im * zdr.im, zst.im * zdr.re + zst.re * zdr.im);
    }

    public static myComplex operator *(myComplex zst, double y)
    {
        return new myComplex(zst.re * y, zst.im * y);
    }
    
    public static myComplex operator /(myComplex zst, myComplex zdr)
    {
        double r = zdr.Ro2;
        if (r < 1.0e-12) return myComplex.setRoTheta(Double.MaxValue, zst.Theta - zdr.Theta);
        return new myComplex((zst.re * zdr.re + zst.im * zdr.im) / r, (zst.im * zdr.re - zst.re * zdr.im) / r);
    }
    
    public static myComplex operator /(myComplex z, double x)
    {
        var result = x / (z.re * z.re + z.im * z.im);
        
        return new myComplex(
            z.re * result,
            -z.im * result);
    }

    public double taylorExp(double x)
    {
        //taylor exp for real numbers;
        double sum = 1;
        for (int i = 999; i > 0; i--)
            sum = 1 + x * sum / i;
        return sum;
    }
    public myComplex Exp()
    {
        //Euler
        double rest = taylorExp(re);
        return new myComplex(
            rest * Math.Cos(im),
            rest * Math.Sin(im)
        );
    }

    public double Arg()
    {
        if (re == 0 && im == 0) return 0;
        
        double arg = Math.Acos(Math.Clamp(re / Abs(), -1, 1));
        
        if (im < 0)
            arg = -arg;
        
        return arg;
    }
    public double Abs()
    {
        return Math.Sqrt(re * re + im * im);
    }
    
    public myComplex Pow(double power)
    {
        double newArg = Arg() * power;
        double newAbs = Math.Pow(Abs(), power);

        return new myComplex(
            newAbs * Math.Cos(newArg),
            newAbs * Math.Sin(newArg)
        );
    }
    public static myComplex cos(myComplex z)
    {
        myComplex poz = new myComplex(z.re, z.im);
        myComplex neg = new myComplex(z.re, -z.im);

        return new myComplex(
            (poz.Exp() + neg.Exp()).re / 2,
            (poz.Exp() + neg.Exp()).im / 2
        );
    }

    public static myComplex sin(myComplex z)
    {
        myComplex poz = new myComplex(z.re, z.im);
        myComplex neg = new myComplex(z.re, -z.im);

        return new myComplex(
            (poz.Exp() - neg.Exp()).re / 2,
            (poz.Exp() - neg.Exp()).im / 2
        );
    }
  
}
