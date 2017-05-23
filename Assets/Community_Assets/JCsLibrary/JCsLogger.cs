//#define LOG4NET // log4net logging active?
//
//
//#define LOG_UNITY // Debug.Log* active?

#define UNITY // running inside the unity runtime environment?

using System;
#if UNITY
using UnityEngine; // you can disable this - recommended when using outside of Unity!
#endif

/// <summary>
///     This is a very simply class to abstract the use of log4net.
///     If log4net is included in the project, you simply use this
///     class as it is. If you remove the log4net.dll from your
///     project, all you have to do is uncomment
///     #define LOG4NET (say //#define LOG4NET)
///     at the top of this file.
///    
///     You should definitely remove log4net when building Web players
///     as it increases the size significantly (around 1 to 2 MB!)
///    
///     You can also use this to use Unity's Debug.Log(...). If you have
///     log4net enabled, you can even use log4net's debug levels for this.
///     You could also use this only to use Debug.Log(...), though,
///     without using log4net at all. The choice is yours!!!
///
///     Version history:
///     1.0 - full log4net-support
///     1.1 - added support for Unity's Debug-logging
///     1.2 - Use proper conditional compilation ;-)
/// </summary>
/// <version>1.2</version>
/// <author>Jashan Chittesh - jc (you know it) ramtiga (dot) com</author>
public class JCsLogger
{
    private const string startmsg
        = "\nlog4net configuration file:\n{0}\n\n"
          + "    =======================================\n"
          + "    === Logging configured successfully ===\n"
          + "    =======================================\n";

#if LOG4NET
    private ILog log;
      #endif

    /// <summary>
    ///     A logger to be used for logging statements in the code.
    ///     It is recommended to follow a pattern for instantiating this:
    ///     <code>
    ///         private static readonly JCsLogger log = new JCsLogger(typeof(YourClassName));
    ///         ...
    ///         log.*(yourLoggingStuff); // Debug/Info/Warn/Error/Fatal[Format]
    ///     </code>
    /// </summary>
    /// <param name="type">the type that is using this logger</param>
    public JCsLogger(Type type)
    {
#if LOG4NET
        log = LogManager.GetLogger(type);
#endif
    }

    /// <summary>
    ///     This is automatically called before the first instance of
    ///     JCsLogger is created, and initializes logging. You can change
    ///     this according to your needs.
    /// </summary>
    static JCsLogger()
    {
#if UNITY
        string configFile = Application.dataPath + "/Plugins/log4net/log4net.xml";
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            configFile = Application.dataPath + "\\Plugins\\log4net\\log4net.xml";
        }
        Configure(configFile);
#endif // UNITY
    }

    public static void ConfigureForServer()
    {
#if UNITY
        Configure(Application.dataPath + "/Configuration/log4net_srv.xml");
#endif
    }

    private static void Configure(string configFile)
    {
#if LOG4NET
        FileInfo fileInfo = new FileInfo(configFile);
        XmlConfigurator.ConfigureAndWatch(fileInfo);
        LogManager.GetLogger(typeof(JCsLogger)).InfoFormat(startmsg, configFile);
#endif
    }

    #region Test if a level is enabled for logging - Only works when log4net is active!

    public bool IsDebugEnabled
    {
        get
        {
            bool result = false;
#if LOG4NET
            result = log.IsDebugEnabled;
#endif
            return result;
        }
    }


    public bool IsInfoEnabled
    {
        get
        {
            bool result = false;
#if LOG4NET
            result = log.IsInfoEnabled;
#endif
            return result;
        }
    }


    public bool IsWarnEnabled
    {
        get
        {
            bool result = false;
#if LOG4NET
            result = log.IsWarnEnabled;
#endif
            return result;
        }
    }


    public bool IsErrorEnabled
    {
        get
        {
            bool result = false;
#if LOG4NET
            result = log.IsErrorEnabled;
#endif
            return result;
        }
    }


    public bool IsFatalEnabled
    {
        get
        {
            bool result = false;
#if LOG4NET
            result = log.IsFatalEnabled;
#endif
            return result;
        }
    }

    #endregion Test if a level is enabled for logging


    /* Log a message object */
    public void Debug(object message)
    {
#if LOG4NET
        log.Debug(message);
#endif
#if LOG_UNITY
        if (IsDebugEnabled)
        {
            UnityEngine.Debug.Log(message);
        }
#endif
    }


    public void Info(object message)
    {
#if LOG4NET
        log.Info(message);
#endif
#if LOG_UNITY
        if (IsInfoEnabled)
        {
            UnityEngine.Debug.Log(message);
        }
#endif
    }


    public void Warn(object message)
    {
#if LOG4NET
        log.Warn(message);
#endif
#if LOG_UNITY
        if (IsWarnEnabled)
        {
            UnityEngine.Debug.LogWarning(message);
        }
#endif
    }


    public void Error(object message)
    {
#if LOG4NET
        log.Error(message);
#endif
#if LOG_UNITY
        if (IsErrorEnabled)
        {
            UnityEngine.Debug.LogError(message);
        }
#endif
    }


    public void Fatal(object message)
    {
#if LOG4NET
        log.Fatal(message);
#endif
#if LOG_UNITY
        if (IsFatalEnabled)
        {
            UnityEngine.Debug.LogError(message);
        }
#endif
    }


    /* Log a message object and exception */
    public void Debug(object message, Exception t)
    {
#if LOG4NET
        log.Debug(message, t);
#endif
#if LOG_UNITY
        if (IsDebugEnabled)
        {
            UnityEngine.Debug.Log(string.Format("{0}\n{1}: {2}\n{3}", message, t.GetType().ToString(), t.Message,
                t.StackTrace));
        }
#endif
    }


    public void Info(object message, Exception t)
    {
#if LOG4NET
        log.Info(message, t);
#endif
#if LOG_UNITY
        if (IsInfoEnabled)
        {
            UnityEngine.Debug.Log(string.Format("{0}\n{1}: {2}\n{3}", message, t.GetType().ToString(), t.Message,
                t.StackTrace));
        }
#endif
    }


    public void Warn(object message, Exception t)
    {
#if LOG4NET
        log.Warn(message, t);
#endif
#if LOG_UNITY
        if (IsWarnEnabled)
        {
            UnityEngine.Debug.LogWarning(string.Format("{0}\n{1}: {2}\n{3}", message, t.GetType().ToString(), t.Message,
                t.StackTrace));
        }
#endif
    }


    public void Error(object message, Exception t)
    {
#if LOG4NET
        log.Error(message, t);
#endif
#if LOG_UNITY
        if (IsErrorEnabled)
        {
            UnityEngine.Debug.LogError(string.Format("{0}\n{1}: {2}\n{3}", message, t.GetType().ToString(), t.Message,
                t.StackTrace));
        }
#endif
    }


    public void Fatal(object message, Exception t)
    {
#if LOG4NET
        log.Fatal(message, t);
#endif
#if LOG_UNITY
        if (IsFatalEnabled)
        {
            UnityEngine.Debug.LogError(string.Format("{0}\n{1}: {2}\n{3}", message, t.GetType().ToString(), t.Message,
                t.StackTrace));
        }
#endif
    }


    /* Log a message string using the System.String.Format syntax */
    public void DebugFormat(string format, params object[] args)
    {
#if LOG4NET
        log.DebugFormat(format, args);
#endif
#if LOG_UNITY
        if (IsDebugEnabled)
        {
            UnityEngine.Debug.Log(string.Format(format, args));
        }
#endif
    }


    public void InfoFormat(string format, params object[] args)
    {
#if LOG4NET
        log.InfoFormat(format, args);
#endif
#if LOG_UNITY
        if (IsInfoEnabled)
        {
            UnityEngine.Debug.Log(string.Format(format, args));
        }
#endif
    }


    public void WarnFormat(string format, params object[] args)
    {
#if LOG4NET
        log.WarnFormat(format, args);
#endif
#if LOG_UNITY
        if (IsWarnEnabled)
        {
            UnityEngine.Debug.LogWarning(string.Format(format, args));
        }
#endif
    }


    public void ErrorFormat(string format, params object[] args)
    {
#if LOG4NET
        log.ErrorFormat(format, args);
#endif
#if LOG_UNITY
        if (IsErrorEnabled)
        {
            UnityEngine.Debug.LogError(string.Format(format, args));
        }
#endif
    }


    public void FatalFormat(string format, params object[] args)
    {
#if LOG4NET
        log.FatalFormat(format, args);
#endif
#if LOG_UNITY
        if (IsFatalEnabled)
        {
            UnityEngine.Debug.LogError(string.Format(format, args));
        }
#endif
    }


    /* Log a message string using the System.String.Format syntax */
    public void DebugFormat(IFormatProvider provider, string format, params object[] args)
    {
#if LOG4NET
        log.DebugFormat(provider, format, args);
#endif
#if LOG_UNITY
        if (IsDebugEnabled)
        {
            UnityEngine.Debug.Log(string.Format(provider, format, args));
        }
#endif
    }


    public void InfoFormat(IFormatProvider provider, string format, params object[] args)
    {
#if LOG4NET
        log.InfoFormat(provider, format, args);
#endif
#if LOG_UNITY
        if (IsInfoEnabled)
        {
            UnityEngine.Debug.Log(string.Format(provider, format, args));
        }
#endif
    }


    public void WarnFormat(IFormatProvider provider, string format, params object[] args)
    {
#if LOG4NET
        log.WarnFormat(provider, format, args);
#endif
#if LOG_UNITY
        if (IsWarnEnabled)
        {
            UnityEngine.Debug.LogWarning(string.Format(provider, format, args));
        }
#endif
    }


    public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
    {
#if LOG4NET
        log.ErrorFormat(provider, format, args);
#endif
#if LOG_UNITY
        if (IsErrorEnabled)
        {
            UnityEngine.Debug.LogError(string.Format(provider, format, args));
        }
#endif
    }


    public void FatalFormat(IFormatProvider provider, string format, params object[] args)
    {
#if LOG4NET
        log.FatalFormat(provider, format, args);
#endif
#if LOG_UNITY
        if (IsFatalEnabled)
        {
            UnityEngine.Debug.LogError(string.Format(provider, format, args));
        }
#endif
    }
}