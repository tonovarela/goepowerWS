﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConsoleApplication2.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.6.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://goepowerlive.goepower.com/webservices/OrderInfo.asmx")]
        public string ConsoleApplication2_servicio_OrderInfo {
            get {
                return ((string)(this["ConsoleApplication2_servicio_OrderInfo"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.2.217;Initial Catalog=Lito;Persist Security Info=True;User ID" +
            "=sa;Password=TcpkfcW8l1t0")]
        public string LitoConnectionString {
            get {
                return ((string)(this["LitoConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.2.217;Initial Catalog=Lito;Persist Security Info=True;User ID" +
            "=sa;Password=TcpkfcW8l1t0")]
        public string LitoPruebasCFDIConnectionString {
            get {
                return ((string)(this["LitoPruebasCFDIConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.2.217;Initial Catalog=LitoPruebas;User ID=sa;Password=TcpkfcW" +
            "8l1t0")]
        public string LitoPruebasConnectionString {
            get {
                return ((string)(this["LitoPruebasConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.2.217;Initial Catalog=GoePower;Persist Security Info=True;Use" +
            "r ID=sa;Password=TcpkfcW8l1t0")]
        public string GoePowerConnectionString {
            get {
                return ((string)(this["GoePowerConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.2.209;Initial Catalog=Goepower;Persist Security Info=True;Use" +
            "r ID=sa;Password=8hub2?_REdu2*")]
        public string GoepowerConnectionString1 {
            get {
                return ((string)(this["GoepowerConnectionString1"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.2.217;Initial Catalog=LitoPruebas;Persist Security Info=True;" +
            "User ID=sa;Password=TcpkfcW8l1t0")]
        public string LitoPruebasConnectionString1 {
            get {
                return ((string)(this["LitoPruebasConnectionString1"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.2.217;Initial Catalog=LitoPrueFact;Persist Security Info=True" +
            ";User ID=sa;Password=TcpkfcW8l1t0")]
        public string LitoPrueFactConnectionString {
            get {
                return ((string)(this["LitoPrueFactConnectionString"]));
            }
        }
    }
}
