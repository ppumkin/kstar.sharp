//// Too many errors in the source code. Stopped converting
//// Please, fix them to proceed
//package com.kstar.common;

//import android.content.Context;
//import android.content.res.Resources;
//import android.database.Cursor;
//import android.database.sqlite.SQLiteDatabase;
//import android.net.ConnectivityManager;
//import android.net.wifi.WifiConfiguration;
//import android.net.wifi.WifiManager;
//import android.net.wifi.WifiManager.WifiLock;
//import android.util.Log;
//import com.kstar.EzManage.MainApplication;
//import com.kstar.EzManage.R;
//import java.io.File;
//import java.io.IOException;
//import java.io.InputStream;
//import java.io.OutputStream;
//import java.io.UnsupportedEncodingException;
//import java.text.SimpleDateFormat;
//import java.util.Calendar;
//import java.util.Date;
//import java.util.HashMap;
//import java.util.UUID;

//public class DataCollectUtil {
//    private static SimpleDateFormat DBsdf = new SimpleDateFormat("yyyy-MM-dd");
//    public static boolean IsConnect = false;
//    public static boolean IsFirst = false;
//    public static boolean IsStart = false;
//    public static boolean Iscancel = false;
//    public static int SendCount = flagcollect;
//    private static final String TAG = "DataCollectUtil";
//    private static final Object cellock = new Object();
//    public static ConnectivityManager connectManager = null;
//    private static Context context = null;
//    private static DataCollectUtil dcUtil = new DataCollectUtil();
//    private static final Object dclock = new Object();
//    private static final int flagcollect = 0;
//    private static final int flagsettime = 1;
//    private static final int flagsetworkmode = 2;
//    public static int iCount = flagcollect;
//    private static final Object reslock = new Object();
//    private static SimpleDateFormat sdf = new SimpleDateFormat("yy-MM-dd HH:mm:ss:ms");
//    public static boolean sendCharge = false;
//    public static boolean sendDischarge = false;
//    private static final Object sendlock = new Object();
//    private static int[] status = new int[3];
//    private static UdpUnicast udp = new UdpUnicast();
//    private static WifiLock wifiLock = null;
//    public static WifiManager wifiManager;
//    private Integer INVENTER_ADDRESS;
//    private String INVENTER_SN = "0123456789ABCDEF";
//    private byte[] INVENTER_SN_BYTE = new byte[]{(byte) 49, (byte) 52, (byte) 48, (byte) 48, (byte) 48, (byte) 68, (byte) 83, (byte) 85, (byte) 49, (byte) 49, (byte) 49, (byte) 49, (byte) 49, (byte) 49, (byte) 49, (byte) 49};
//    private byte[] buffer;
//    private InputStream is = null;
//    private OutputStream os = null;
//    private Resources res = null;

//    public static // [60,20] Unknown token: "--\u: voi// [60,56] Unknown token: "--\u: d dataCollect(Context ctx) throws Exception {
//        Log.d/* [60,14] expecting: identifier: (*/TAG, "--\u6570\u636e\u91c7\u96c6\u5f00\u59cb");
//        dcUtil.init(ctx);
//        if (!wifiManager.isWifiEnabled()) {
//            Log.d(TAG, "WiFi closed");
//            Constant.reconnectionFlag = true;
//            MainApplication.WriterLog(new StringBuilder(String.valueOf(sdf.format(new Date(System.currentTimeMillis())))).append("WiFi closed").append('\n').toString());
//        }
//        if (Constant.collectMode == flagsettime) {
//            if (iCount == 0 || Constant.BroadcastFailCount >= flagsetworkmode || Constant.CollectorFailCount >= 3) {
//                MainApplication.mScanBroadcast.open();
//                MainApplication.mScanBroadcast.send(Utils.getCMDScanModules());
//                Constant.reconnectionFlag = true;
//                MainApplication.WriterLog(new StringBuilder(String.valueOf(sdf.format(new Date(System.currentTimeMillis())))).append("-BroadcastCmd:->").append(Constant.BroadcastCmd).append("; FailCount=").append(Constant.BroadcastFailCount).append('\n').toString());
//                Log.d(TAG, "--111Constant.BroadcastFailCount-" + Constant.BroadcastFailCount);
//                Log.d(TAG, "--222Constant.CollectorFailCount-" + Constant.CollectorFailCount);
//            }
//            if (MainApplication.mModules == null || MainApplication.mModules.size() <= 0) {
//                Constant.BroadcastFailCount += flagsettime;
//                if (Constant.BroadcastFailCount > 4) {
//                    Constant.BroadcastFailCount = flagsetworkmode;
//                }
//                Log.d(TAG, "-----BroadcastFailCount----" + Constant.BroadcastFailCount);
//            } else {
//                for (int p = flagcollect; p < MainApplication.mModules.size(); p += flagsettime) {
//                    Log.d(TAG, "--ScanBroadcast Recevice: WiFi module info :" + MainApplication.mModules.get(p));
//                    Constant.TCP_SERVER_IP = ((Module) MainApplication.mModules.get(p)).getIp();
//                    Constant.TCP_SERVER_MAC = ((Module) MainApplication.mModules.get(p)).getMac();
//                    Constant.TCP_SERVER_MODULE_SN = ((Module) MainApplication.mModules.get(p)).getModuleID();
//                    udp.setIp(Constant.TCP_SERVER_IP);
//                    udp.open();
//                    Constant.BroadcastFailCount = flagcollect;
//                    mcollectData();
//                }
//            }
//            IsFirst = false;
//        }
//        Log.d(TAG, "\u6570\u636e\u91c7\u96c6\u7ed3\u675f");
//    }

//    private static void mcollectData() throws Exception {
//        status[flagcollect] = flagsettime;
//        if ((Constant.Inverter_sn == null || Constant.Inverter_sn.length() != 16 || Constant.reconnectionFlag) && GetIDInfo()) {
//            Utils.syncInverterError(context);
//            Constant.reconnectionFlag = false;
//        }
//        Log.d(TAG, "\u83b7\u53d6\u6570\u636e\u5305");
//        Log.d(TAG, "--Constant.CollectorFailCount = " + Constant.CollectorFailCount);
//        byte[] inventerData = dcUtil.getDataPackage();
//        Log.d(TAG, "\u83b7\u53d6\u6570\u636e\u5305\u6210\u529f");
//        if (dcUtil.checkPackageType(inventerData) == 9) {
//            Log.d(TAG, "\u5f00\u59cb\u5904\u7406\u6570\u636e\u5305");
//            boolean saFlag = dcUtil.saveInventerData(inventerData);
//            Log.d(TAG, "\u6570\u636e\u5305\u5904\u7406\u5b8c\u6bd5");
//            if (saFlag) {
//                Constant.CollectorFailCount = flagcollect;
//                Constant.fristFlag = false;
//            } else {
//                Constant.CollectorFailCount += flagsettime;
//            }
//            iCount = flagsettime;
//            return;
//        }
//        Constant.CollectorFailCount += flagsettime;
//        if (iCount == 0) {
//            iCount = flagsettime;
//            Constant.fristFlag = true;
//            Log.d(TAG, "--Constant.fristFlag =--" + Constant.fristFlag);
//        }
//        if (Constant.CollectorFailCount >= 13) {
//            Constant.CollectorFailCount = 10;
//        }
//        Log.d(TAG, "-CollectorFailCount--" + Constant.CollectorFailCount);
//        MainApplication.WriterLog(new StringBuilder(String.valueOf(sdf.format(new Date(System.currentTimeMillis())))).append("-CollectorFailCount--").append(Constant.CollectorFailCount).append('\n').toString());
//    }

//    public static boolean GetSettingData() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(Constant.GetSetting_REQUEST_PACKAGE));
//            if (dcUtil.checkPackageType(confirmData) == 104) {
//                byte[] data = dcUtil.fetchData(confirmData);
//                if (data != null) {
//                    Byte valueOf;
//                    byte bChargeStartHour = data[flagcollect];
//                    byte bChargeStartMinute = data[flagsettime];
//                    byte bChargeEndHour = data[flagsetworkmode];
//                    byte bChargeEndMinute = data[3];
//                    byte bDisChargeStartHour = data[6];
//                    byte bDisChargeStartMinute = data[7];
//                    byte bDisChargeEndHour = data[8];
//                    byte bDisChargeEndMinute = data[9];
//                    if (bChargeStartHour == (byte) 48) {
//                        bChargeStartHour = (byte) 0;
//                        bChargeStartMinute = (byte) 0;
//                    }
//                    if (bChargeEndHour == (byte) 48) {
//                        bChargeEndHour = (byte) 0;
//                        bChargeEndMinute = (byte) 0;
//                    }
//                    if (bDisChargeStartHour == (byte) 48) {
//                        bDisChargeStartHour = (byte) 0;
//                        bDisChargeStartMinute = (byte) 0;
//                    }
//                    if (bDisChargeEndHour == (byte) 48) {
//                        bDisChargeEndHour = (byte) 0;
//                        bDisChargeEndMinute = (byte) 0;
//                    }
//                    Constant.Time_begin_charge = new StringBuilder(String.valueOf(bChargeStartHour)).append(":").append(bChargeStartMinute > (byte) 9 ? Byte.valueOf(bChargeStartMinute) : new StringBuilder(Constant.btn_stationdetail_power_QueryYear).append(bChargeStartMinute).toString()).toString();
//                    Constant.Time_end_charge = new StringBuilder(String.valueOf(bChargeEndHour)).append(":").append(bChargeEndMinute > (byte) 9 ? Byte.valueOf(bChargeEndMinute) : new StringBuilder(Constant.btn_stationdetail_power_QueryYear).append(bChargeEndMinute).toString()).toString();
//                    Constant.Time_begin_discharge = new StringBuilder(String.valueOf(bDisChargeStartHour)).append(":").append(bDisChargeStartMinute > (byte) 9 ? Byte.valueOf(bDisChargeStartMinute) : new StringBuilder(Constant.btn_stationdetail_power_QueryYear).append(bDisChargeStartMinute).toString()).toString();
//                    StringBuilder append = new StringBuilder(String.valueOf(bDisChargeEndHour)).append(":");
//                    if (bDisChargeEndMinute > (byte) 9) {
//                        valueOf = Byte.valueOf(bDisChargeEndMinute);
//                    } else {
//                        valueOf = new StringBuilder(Constant.btn_stationdetail_power_QueryYear).append(bDisChargeEndMinute).toString();
//                    }
//                    Constant.Time_end_discharge = append.append(valueOf).toString();
//                    DataCollectUtil dataCollectUtil = dcUtil;
//                    byte[] bArr = new byte[flagsetworkmode];
//                    bArr[flagcollect] = data[4];
//                    bArr[flagsettime] = data[5];
//                    int iChargePower = dataCollectUtil.byte2ToInt(bArr);
//                    dataCollectUtil = dcUtil;
//                    bArr = new byte[flagsetworkmode];
//                    bArr[flagcollect] = data[10];
//                    bArr[flagsettime] = data[11];
//                    int iDisChargePower = dataCollectUtil.byte2ToInt(bArr);
//                    DataCollectUtil dataCollectUtil2 = dcUtil;
//                    byte[] bArr2 = new byte[flagsetworkmode];
//                    bArr2[flagcollect] = data[4];
//                    bArr2[flagsettime] = data[5];
//                    Constant.ChargePowerLimitValue = new StringBuilder(String.valueOf(dataCollectUtil2.byte2ToInt(bArr2))).toString();
//                    dataCollectUtil2 = dcUtil;
//                    bArr2 = new byte[flagsetworkmode];
//                    bArr2[flagcollect] = data[10];
//                    bArr2[flagsettime] = data[11];
//                    Constant.DischargePowerLimitValue = new StringBuilder(String.valueOf(dataCollectUtil2.byte2ToInt(bArr2))).toString();
//                    PropertyUtil.SetValue(context, "Time_begin_charge", Constant.Time_begin_charge);
//                    PropertyUtil.SetValue(context, "Time_end_charge", Constant.Time_end_charge);
//                    PropertyUtil.SetValue(context, "Time_begin_discharge", Constant.Time_begin_discharge);
//                    PropertyUtil.SetValue(context, "Time_end_discharge", Constant.Time_end_discharge);
//                    PropertyUtil.SetValue(context, "ChargePowerLimitValue", Constant.ChargePowerLimitValue);
//                    PropertyUtil.SetValue(context, "DischargePowerLimitValue", Constant.DischargePowerLimitValue);
//                    if (data[13] == (byte) 0) {
//                        PropertyUtil.SetValue(context, "bRelayControlLow", "false");
//                        Constant.bRelayControlLow = false;
//                        Constant.BackupStateFlag = flagcollect;
//                    } else if (data[13] == flagsettime) {
//                        PropertyUtil.SetValue(context, "bRelayControlLow", "true");
//                        Constant.bRelayControlLow = true;
//                        Constant.BackupStateFlag = flagsetworkmode;
//                    }
//                    if (data[15] == flagsettime) {
//                        PropertyUtil.SetValue(context, "Off_GridchargeStateFlag", Constant.btn_stationdetail_power_QueryDay);
//                        Constant.Off_GridchargeStateFlag = flagsetworkmode;
//                    } else if (data[15] == (byte) 0) {
//                        PropertyUtil.SetValue(context, "Off_GridchargeStateFlag", Constant.btn_stationdetail_power_QueryYear);
//                        Constant.Off_GridchargeStateFlag = flagcollect;
//                    }
//                    if (data[17] == flagsettime) {
//                        PropertyUtil.SetValue(context, "ShadowscanStateFlag", Constant.btn_stationdetail_power_QueryDay);
//                        Constant.ShadowscanStateFlag = flagsetworkmode;
//                    } else {
//                        PropertyUtil.SetValue(context, "ShadowscanStateFlag", Constant.btn_stationdetail_power_QueryYear);
//                        Constant.ShadowscanStateFlag = flagcollect;
//                    }
//                    dataCollectUtil = dcUtil;
//                    bArr = new byte[flagsetworkmode];
//                    bArr[flagcollect] = data[18];
//                    bArr[flagsettime] = data[19];
//                    if (dataCollectUtil.byte2ToInt(bArr) == flagsettime) {
//                        PropertyUtil.SetValue(context, "BackflowStateFlag", Constant.btn_stationdetail_power_QueryYear);
//                        Constant.BackflowStateFlag = flagcollect;
//                    } else {
//                        PropertyUtil.SetValue(context, "BackflowStateFlag", Constant.btn_stationdetail_power_QueryDay);
//                        Constant.BackflowStateFlag = flagsetworkmode;
//                    }
//                    dataCollectUtil = dcUtil;
//                    bArr = new byte[flagsetworkmode];
//                    bArr[flagcollect] = data[22];
//                    bArr[flagsettime] = data[23];
//                    int LeadBatCapacity = dataCollectUtil.byte2ToInt(bArr);
//                    Constant.CapacityValue_back = LeadBatCapacity;
//                    PropertyUtil.SetValue(context, "BatteryCapcityValue", String.valueOf(LeadBatCapacity));
//                    dataCollectUtil = dcUtil;
//                    bArr = new byte[flagsetworkmode];
//                    bArr[flagcollect] = data[24];
//                    bArr[flagsettime] = data[25];
//                    int LeadchargeV = dataCollectUtil.byte2ToInt(bArr);
//                    Constant.Charge_V_Value_back = (double) LeadchargeV;
//                    PropertyUtil.SetValue(context, "LeadchargeV", String.valueOf(LeadchargeV));
//                    dataCollectUtil = dcUtil;
//                    bArr = new byte[flagsetworkmode];
//                    bArr[flagcollect] = data[26];
//                    bArr[flagsettime] = data[27];
//                    int LeadchargeI = dataCollectUtil.byte2ToInt(bArr);
//                    Constant.Charge_I_Value_back = ((double) LeadchargeI) / 0.266d;
//                    PropertyUtil.SetValue(context, "LeadchargeI", String.valueOf(LeadchargeI));
//                    dataCollectUtil = dcUtil;
//                    bArr = new byte[flagsetworkmode];
//                    bArr[flagcollect] = data[30];
//                    bArr[flagsettime] = data[31];
//                    int LeadDischargeV = dataCollectUtil.byte2ToInt(bArr);
//                    Constant.Discharge_V_Value_back = (double) LeadDischargeV;
//                    PropertyUtil.SetValue(context, "LeadDischargeV", String.valueOf(LeadDischargeV));
//                    dataCollectUtil = dcUtil;
//                    bArr = new byte[flagsetworkmode];
//                    bArr[flagcollect] = data[28];
//                    bArr[flagsettime] = data[29];
//                    int LeadDischargeI = dataCollectUtil.byte2ToInt(bArr);
//                    Constant.Discharge_I_Value_back = ((double) LeadDischargeI) / 0.128d;
//                    PropertyUtil.SetValue(context, "LeadDischargeI", String.valueOf(LeadDischargeI));
//                    dataCollectUtil = dcUtil;
//                    bArr = new byte[flagsetworkmode];
//                    bArr[flagcollect] = data[32];
//                    bArr[flagsettime] = data[33];
//                    int LeadDepthdischarge = dataCollectUtil.byte2ToInt(bArr);
//                    Constant.Depth_Discharge_Value_back = LeadDepthdischarge;
//                    PropertyUtil.SetValue(context, "LeadDepthdischarge", String.valueOf(LeadDepthdischarge));
//                    try {
//                        MainApplication.WriterLog(new StringBuilder(String.valueOf(sdf.format(new Date(System.currentTimeMillis())))).append("--Get Charge_V:").append(Constant.Charge_V_Value_back).append(";Charge_I:").append(Constant.Charge_I_Value_back).append(";Discharge_V:").append(Constant.Discharge_V_Value_back).append(";Discharge_I:").append(Constant.Discharge_I_Value_back).append(";Depth_Discharge:").append(Constant.Depth_Discharge_Value_back).append(";Capcity:").append(Constant.CapacityValue_back).append('\n').toString());
//                    } catch (IOException e) {
//                        Log.e(TAG, e.toString());
//                    }
//                    return true;
//                }
//                Constant.CapacityValue_back = Constant.CapacityValue_set;
//                Constant.Charge_V_Value_back = Constant.Charge_V_Value_set;
//                Constant.Charge_I_Value_back = Constant.Charge_I_Value_set;
//                Constant.Discharge_V_Value_back = Constant.Discharge_V_Value_set;
//                Constant.Discharge_I_Value_back = Constant.Discharge_I_Value_set;
//                Constant.Depth_Discharge_Value_back = 100 - Constant.Depth_Discharge_Value_set;
//            }
//            try {
//                MainApplication.WriterLog(new StringBuilder(String.valueOf(sdf.format(new Date(System.currentTimeMillis())))).append("--Get Charge_V:").append(Constant.Charge_V_Value_back).append(";Charge_I:").append(Constant.Charge_I_Value_back).append(";Discharge_V:").append(Constant.Discharge_V_Value_back).append(";Discharge_I:").append(Constant.Discharge_I_Value_back).append(";Depth_Discharge:").append(Constant.Depth_Discharge_Value_back).append(";Capcity:").append(Constant.CapacityValue_back).append('\n').toString());
//                return false;
//            } catch (IOException e2) {
//                Log.e(TAG, e2.toString());
//                return false;
//            }
//        } catch (Exception e3) {
//            Constant.CapacityValue_back = Constant.CapacityValue_set;
//            Constant.Charge_V_Value_back = Constant.Charge_V_Value_set;
//            Constant.Charge_I_Value_back = Constant.Charge_I_Value_set;
//            Constant.Discharge_V_Value_back = Constant.Discharge_V_Value_set;
//            Constant.Discharge_I_Value_back = Constant.Discharge_I_Value_set;
//            Constant.Depth_Discharge_Value_back = 100 - Constant.Depth_Discharge_Value_set;
//            Log.e(TAG, e3.toString());
//            try {
//                MainApplication.WriterLog(new StringBuilder(String.valueOf(sdf.format(new Date(System.currentTimeMillis())))).append("--Get Charge_V:").append(Constant.Charge_V_Value_back).append(";Charge_I:").append(Constant.Charge_I_Value_back).append(";Discharge_V:").append(Constant.Discharge_V_Value_back).append(";Discharge_I:").append(Constant.Discharge_I_Value_back).append(";Depth_Discharge:").append(Constant.Depth_Discharge_Value_back).append(";Capcity:").append(Constant.CapacityValue_back).append('\n').toString());
//                return false;
//            } catch (IOException e22) {
//                Log.e(TAG, e22.toString());
//                return false;
//            }
//        } catch (Throwable th) {
//            try {
//                MainApplication.WriterLog(new StringBuilder(String.valueOf(sdf.format(new Date(System.currentTimeMillis())))).append("--Get Charge_V:").append(Constant.Charge_V_Value_back).append(";Charge_I:").append(Constant.Charge_I_Value_back).append(";Discharge_V:").append(Constant.Discharge_V_Value_back).append(";Discharge_I:").append(Constant.Discharge_I_Value_back).append(";Depth_Discharge:").append(Constant.Depth_Discharge_Value_back).append(";Capcity:").append(Constant.CapacityValue_back).append('\n').toString());
//            } catch (IOException e222) {
//                Log.e(TAG, e222.toString());
//            }
//        }
//    }

//    private void init(Context ctx) throws IOException {
//        this.res = ctx.getResources();
//        context = ctx;
//        if (iCount == 0) {
//            wifiManager = (WifiManager) context.getSystemService("wifi");
//            connectManager = (ConnectivityManager) context.getSystemService("connectivity");
//            String wifiID = wifiManager.getConnectionInfo().getSSID();
//            if (wifiID != null) {
//                MainApplication.WriterLog(new StringBuilder(String.valueOf(sdf.format(new Date(System.currentTimeMillis())))).append("----------WiFi SSID: ").append(wifiID).append("------\n").toString());
//            }
//        }
//    }

//    private boolean register(int flag) throws Exception {
//        synchronized (reslock) {
//            for (int i = flagcollect; i < status.length; i += flagsettime) {
//                if (status[i] == flagsettime) {
//                    status[flag] = flagsettime;
//                    return true;
//                }
//            }
//            byte[] temp = sendForData(buildPackage(flagcollect));
//            if (checkPackageType(temp) == flagsettime) {
//                iniDataCollection(temp, null);
//                byte[] addressData = buildPackage(flagsetworkmode);
//                if (checkPackageType(sendForData(addressData)) == 3) {
//                    iniDataCollection(null, addressData);
//                    status[flag] = flagsettime;
//                    Iscancel = false;
//                    return true;
//                }
//            }
//            return false;
//        }
//    }

//    private void iniDataCollection(byte[] requestData, byte[] addressData) {
//        if (requestData != null && requestData.length > 0) {
//            byte[] rqData = fetchData(requestData);
//            this.INVENTER_SN_BYTE = rqData;
//            if (rqData != null) {
//                String temp = StringUtil.EMPTY;
//                for (int i = flagcollect; i < rqData.length; i += flagsettime) {
//                    temp = new StringBuilder(String.valueOf(temp)).append((char) rqData[i]).toString();
//                }
//                this.INVENTER_SN = temp;
//            }
//        }
//        if (addressData != null && addressData.length > 0) {
//            byte[] adData = fetchData(addressData);
//            if (adData != null) {
//                this.INVENTER_ADDRESS = Integer.valueOf(byteToInt(adData[adData.length - 1]));
//            }
//        }
//    }

//    private byte[] buildPackage(int pkgType) {
//        Byte hHead = Byte.valueOf(Constant.INVENTERPACKAGE_HEAD[flagcollect]);
//        Byte lHead = Byte.valueOf(Constant.INVENTERPACKAGE_HEAD[flagsettime]);
//        Byte source = Byte.valueOf(Constant.MONITOR_ADDRESS.byteValue());
//        Byte dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//        byte[] checkSum = new byte[flagsetworkmode];
//        Byte ccode;
//        Byte fcode;
//        Byte dataLength;
//        int iLength;
//        Integer sum;
//        int iIndex;
//        Object pkg;
//        switch (pkgType) {
//            case flagcollect /*0*/:
//                checkSum = intTo2Byte(Integer.valueOf((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue()).byteValue())) + byteToInt(Byte.valueOf(Constant.REGISTER_QUERY_CODE[flagcollect]).byteValue())) + byteToInt(Byte.valueOf(Constant.REGISTER_QUERY_CODE[flagsettime]).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryYear).byteValue()).byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case flagsetworkmode /*2*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.REGISTER_ADDRESS_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.REGISTER_ADDRESS_CODE[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf("17").byteValue());
//                if (this.INVENTER_SN_BYTE == null || this.INVENTER_SN_BYTE.length != 16) {
//                    return null;
//                }
//                checkSum = intTo2Byte(Integer.valueOf(((((((((((((((((((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())) + byteToInt(this.INVENTER_SN_BYTE[flagcollect])) + byteToInt(this.INVENTER_SN_BYTE[flagsettime])) + byteToInt(this.INVENTER_SN_BYTE[flagsetworkmode])) + byteToInt(this.INVENTER_SN_BYTE[3])) + byteToInt(this.INVENTER_SN_BYTE[4])) + byteToInt(this.INVENTER_SN_BYTE[5])) + byteToInt(this.INVENTER_SN_BYTE[6])) + byteToInt(this.INVENTER_SN_BYTE[7])) + byteToInt(this.INVENTER_SN_BYTE[8])) + byteToInt(this.INVENTER_SN_BYTE[9])) + byteToInt(this.INVENTER_SN_BYTE[10])) + byteToInt(this.INVENTER_SN_BYTE[11])) + byteToInt(this.INVENTER_SN_BYTE[12])) + byteToInt(this.INVENTER_SN_BYTE[13])) + byteToInt(this.INVENTER_SN_BYTE[14])) + byteToInt(this.INVENTER_SN_BYTE[15])) + byteToInt(Byte.valueOf(Integer.valueOf((((int) Math.random()) * 50) + flagsettime).byteValue()).byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), this.INVENTER_SN_BYTE[flagcollect], this.INVENTER_SN_BYTE[flagsettime], this.INVENTER_SN_BYTE[flagsetworkmode], this.INVENTER_SN_BYTE[3], this.INVENTER_SN_BYTE[4], this.INVENTER_SN_BYTE[5], this.INVENTER_SN_BYTE[6], this.INVENTER_SN_BYTE[7], this.INVENTER_SN_BYTE[8], this.INVENTER_SN_BYTE[9], this.INVENTER_SN_BYTE[10], this.INVENTER_SN_BYTE[11], this.INVENTER_SN_BYTE[12], this.INVENTER_SN_BYTE[13], this.INVENTER_SN_BYTE[14], this.INVENTER_SN_BYTE[15], address.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case InverterMode.InverterMode_Mode4 /*4*/:
//                checkSum = intTo2Byte(Integer.valueOf((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue()).byteValue())) + byteToInt(Byte.valueOf(Constant.CANCEL_REQUEST_CODE[flagcollect]).byteValue())) + byteToInt(Byte.valueOf(Constant.CANCEL_REQUEST_CODE[flagsettime]).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryYear).byteValue()).byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case InverterMode.InverterMode_Mode6 /*6*/:
//                checkSum = intTo2Byte(Integer.valueOf((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue()).byteValue())) + byteToInt(Byte.valueOf(Constant.REQUEST_INDEX_CODE[flagcollect]).byteValue())) + byteToInt(Byte.valueOf(Constant.REQUEST_INDEX_CODE[flagsettime]).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryYear).byteValue()).byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case InverterMode.InverterMode_Mode8 /*8*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.REQUEST_DATA_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.REQUEST_DATA_CODE[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryYear).byteValue());
//                Constant.sendType = flagsettime;
//                checkSum = intTo2Byte(Integer.valueOf((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case InverterMode.InverterMode_Mode10 /*10*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.SETTIME_REQUEST_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.SETTIME_REQUEST_CODE[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf("6").byteValue());
//                Calendar cal = Calendar.getInstance();
//                checkSum = intTo2Byte(Integer.valueOf((((((((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(cal.get(flagsettime) - 2000).byteValue()).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(cal.get(flagsetworkmode) + flagsettime).byteValue()).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(cal.get(5)).byteValue()).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(cal.get(11)).byteValue()).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(cal.get(12)).byteValue()).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(cal.get(13)).byteValue()).byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), year.byteValue(), month.byteValue(), day.byteValue(), hour.byteValue(), min.byteValue(), sen.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case InverterMode.InverterMode_Mode12 /*12*/:
//                checkSum = intTo2Byte(Integer.valueOf(((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue()).byteValue())) + byteToInt(Byte.valueOf(Constant.SWITCH_SCI_REQUEST_CODE[flagcollect]).byteValue())) + byteToInt(Byte.valueOf(Constant.SWITCH_SCI_REQUEST_CODE[flagsettime]).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryMonth).byteValue()).byteValue())) + byteToInt(Byte.valueOf((byte) 1).byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), bData.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case InverterMode.InverterMode_Mode14 /*14*/:
//                checkSum = intTo2Byte(Integer.valueOf((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue()).byteValue())) + byteToInt(Byte.valueOf(Constant.REQUEST_IDInfo_CODE[flagcollect]).byteValue())) + byteToInt(Byte.valueOf(Constant.REQUEST_IDInfo_CODE[flagsettime]).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryYear).byteValue()).byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.LimitPower_REQUEST_PACKAGE /*16*/:
//                checkSum = intTo2Byte(Integer.valueOf(((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue()).byteValue())) + byteToInt(Byte.valueOf(Constant.LimitPower_REQUEST_CODE[flagcollect]).byteValue())) + byteToInt(Byte.valueOf(Constant.LimitPower_REQUEST_CODE[flagsettime]).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryMonth).byteValue()).byteValue())) + Constant.TempValue.byteValue()).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), Constant.TempValue.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.ETOTAL_H_INDEX /*18*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.BMSCmd_REQUEST_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.BMSCmd_REQUEST_CODE[flagsettime]);
//                iLength = BatterySelect.bBMSCmd.length;
//                dataLength = Byte.valueOf(intToByte(iLength));
//                sum = Integer.valueOf((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue()));
//                for (iIndex = flagcollect; iIndex < iLength; iIndex += flagsettime) {
//                    sum = Integer.valueOf(sum.intValue() + byteToInt(BatterySelect.bBMSCmd[iIndex]));
//                }
//                checkSum = intTo2Byte(sum.intValue());
//                pkg = new byte[(iLength + 9)];
//                pkg[flagcollect] = hHead.byteValue();
//                pkg[flagsettime] = lHead.byteValue();
//                pkg[flagsetworkmode] = source.byteValue();
//                pkg[3] = dest.byteValue();
//                pkg[4] = ccode.byteValue();
//                pkg[5] = fcode.byteValue();
//                pkg[6] = dataLength.byteValue();
//                System.arraycopy(BatterySelect.bBMSCmd, flagcollect, pkg, 7, dataLength.byteValue());
//                pkg[dataLength.byteValue() + 7] = checkSum[flagcollect];
//                pkg[dataLength.byteValue() + 8] = checkSum[flagsettime];
//                return pkg;
//            case Constant.HTOTAL_H_INDEX /*20*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.BatteryInformation_REQUEST_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.BatteryInformation_REQUEST_CODE[flagsettime]);
//                iLength = BatterySelect.bBatteryInformation.length;
//                dataLength = Byte.valueOf(intToByte(iLength));
//                sum = Integer.valueOf((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue()));
//                for (iIndex = flagcollect; iIndex < iLength; iIndex += flagsettime) {
//                    sum = Integer.valueOf(sum.intValue() + byteToInt(BatterySelect.bBatteryInformation[iIndex]));
//                }
//                checkSum = intTo2Byte(sum.intValue());
//                pkg = new byte[(iLength + 9)];
//                pkg[flagcollect] = hHead.byteValue();
//                pkg[flagsettime] = lHead.byteValue();
//                pkg[flagsetworkmode] = source.byteValue();
//                pkg[3] = dest.byteValue();
//                pkg[4] = ccode.byteValue();
//                pkg[5] = fcode.byteValue();
//                pkg[6] = dataLength.byteValue();
//                System.arraycopy(BatterySelect.bBatteryInformation, flagcollect, pkg, 7, dataLength.byteValue());
//                pkg[dataLength.byteValue() + 7] = checkSum[flagcollect];
//                pkg[dataLength.byteValue() + 8] = checkSum[flagsettime];
//                return pkg;
//            case Constant.BMSReplyPacket_REQUEST_PACKAGE /*22*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.BMSReplyPacket_REQUEST_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.BMSReplyPacket_REQUEST_CODE[flagsettime]);
//                iLength = BatterySelect.bBMSReplyPacket.length;
//                dataLength = Byte.valueOf(intToByte(iLength));
//                sum = Integer.valueOf((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue()));
//                for (iIndex = flagcollect; iIndex < iLength; iIndex += flagsettime) {
//                    sum = Integer.valueOf(sum.intValue() + byteToInt(BatterySelect.bBMSReplyPacket[iIndex]));
//                }
//                checkSum = intTo2Byte(sum.intValue());
//                pkg = new byte[(iLength + 9)];
//                pkg[flagcollect] = hHead.byteValue();
//                pkg[flagsettime] = lHead.byteValue();
//                pkg[flagsetworkmode] = source.byteValue();
//                pkg[3] = dest.byteValue();
//                pkg[4] = ccode.byteValue();
//                pkg[5] = fcode.byteValue();
//                pkg[6] = dataLength.byteValue();
//                System.arraycopy(BatterySelect.bBMSReplyPacket, flagcollect, pkg, 7, dataLength.byteValue());
//                pkg[dataLength.byteValue() + 7] = checkSum[flagcollect];
//                pkg[dataLength.byteValue() + 8] = checkSum[flagsettime];
//                return pkg;
//            case Constant.SWITCH_SCI_REQUEST_PACKAGE1 /*24*/:
//                checkSum = intTo2Byte(Integer.valueOf(((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue()).byteValue())) + byteToInt(Byte.valueOf(Constant.SWITCH_SCI_REQUEST_CODE[flagcollect]).byteValue())) + byteToInt(Byte.valueOf(Constant.SWITCH_SCI_REQUEST_CODE[flagsettime]).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryMonth).byteValue()).byteValue())) + byteToInt(Byte.valueOf((byte) 0).byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), bData1.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.SetStoreEnergyMode_REQUEST_PACKAGE /*47*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.REQUEST_SetStoreEnergyMode_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.REQUEST_SetStoreEnergyMode_CODE[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryMonth).byteValue());
//                Byte bMode = Byte.valueOf((byte) 0);
//                if (Constant.SelectStoreEnergyMode == flagsettime) {
//                    bMode = Byte.valueOf((byte) 2);
//                } else if (Constant.SelectStoreEnergyMode == 0) {
//                    bMode = Byte.valueOf((byte) 4);
//                } else if (Constant.SelectStoreEnergyMode == flagsetworkmode) {
//                    bMode = Byte.valueOf((byte) 8);
//                } else if (Constant.SelectStoreEnergyMode == 3) {
//                    bMode = Byte.valueOf((byte) 1);
//                }
//                checkSum = intTo2Byte(Integer.valueOf(((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())) + byteToInt(bMode.byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), bMode.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.Guanji_REQUEST_PACKAGE /*49*/:
//                checkSum = intTo2Byte(Integer.valueOf(((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue()).byteValue())) + byteToInt(Byte.valueOf(Constant.REQUEST_SetStoreEnergyMode_CODE[flagcollect]).byteValue())) + byteToInt(Byte.valueOf(Constant.REQUEST_SetStoreEnergyMode_CODE[flagsettime]).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryMonth).byteValue()).byteValue())) + byteToInt(Byte.valueOf((byte) 1).byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), bMode2.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.SetRelayControl_REQUEST_PACKAGE /*51*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.REQUEST_SetRelayControl_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.REQUEST_SetRelayControl_CODE[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryDay).byteValue());
//                Constant.BackupOrOffchargeFlag = flagsettime;
//                Byte bRelayControlHigh = Byte.valueOf((byte) 0);
//                Byte bRelayControlLow = Byte.valueOf((byte) 0);
//                if (Constant.SelectRelayControl == flagsetworkmode) {
//                    if (Constant.Off_GridchargeStateFlag == 0) {
//                        bRelayControlLow = Byte.valueOf((byte) 0);
//                    } else {
//                        bRelayControlLow = Byte.valueOf((byte) 16);
//                    }
//                    PropertyUtil.SetValue(context, "bRelayControlLow", "false");
//                } else if (Constant.SelectRelayControl == 3) {
//                    if (Constant.Off_GridchargeStateFlag == 0) {
//                        bRelayControlLow = Byte.valueOf((byte) 32);
//                    } else {
//                        bRelayControlLow = Byte.valueOf((byte) 48);
//                    }
//                    PropertyUtil.SetValue(context, "bRelayControlLow", "true");
//                }
//                checkSum = intTo2Byte(Integer.valueOf((((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())) + byteToInt(bRelayControlHigh.byteValue())) + byteToInt(bRelayControlLow.byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), bRelayControlHigh.byteValue(), bRelayControlLow.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.LimitPower_REQUEST_PACKAGE_CHARGE /*53*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.LimitPower_REQUEST_CODE_CHARGE[flagcollect]);
//                fcode = Byte.valueOf(Constant.LimitPower_REQUEST_CODE_CHARGE[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf("5").byteValue());
//                String[] tempBeginTime = Constant.Time_begin_charge_set.split("\\:");
//                String[] tempEndTime = Constant.Time_end_charge_set.split("\\:");
//                Byte bBeginHour = Byte.valueOf((byte) Integer.valueOf(tempBeginTime[flagcollect]).intValue());
//                Byte bBeginMinute = Byte.valueOf((byte) Integer.valueOf(tempBeginTime[flagsettime]).intValue());
//                Byte bEndHour = Byte.valueOf((byte) Integer.valueOf(tempEndTime[flagcollect]).intValue());
//                Byte bEndMinute = Byte.valueOf((byte) Integer.valueOf(tempEndTime[flagsettime]).intValue());
//                checkSum = intTo2Byte(Integer.valueOf(((((((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())) + Byte.valueOf(Integer.valueOf(Constant.ChargePowerLimitValue_set).byteValue()).byteValue()) + byteToInt(bBeginHour.byteValue())) + byteToInt(bBeginMinute.byteValue())) + byteToInt(bEndHour.byteValue())) + byteToInt(bEndMinute.byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), bBeginHour.byteValue(), bBeginMinute.byteValue(), bEndHour.byteValue(), bEndMinute.byteValue(), bChargePowerRate.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.LimitPower_REQUEST_PACKAGE_DISCHARGE /*55*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.LimitPower_REQUEST_CODE_DISCHARGE[flagcollect]);
//                fcode = Byte.valueOf(Constant.LimitPower_REQUEST_CODE_DISCHARGE[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf("5").byteValue());
//                String[] tempBeginTime1 = Constant.Time_begin_discharge_set.split("\\:");
//                String[] tempEndTime1 = Constant.Time_end_discharge_set.split("\\:");
//                Byte bBeginHour1 = Byte.valueOf(Integer.valueOf(tempBeginTime1[flagcollect]).byteValue());
//                Byte bBeginMinute1 = Byte.valueOf(Integer.valueOf(tempBeginTime1[flagsettime]).byteValue());
//                Byte bEndHour1 = Byte.valueOf(Integer.valueOf(tempEndTime1[flagcollect]).byteValue());
//                Byte bEndMinute1 = Byte.valueOf(Integer.valueOf(tempEndTime1[flagsettime]).byteValue());
//                checkSum = intTo2Byte(Integer.valueOf(((((((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())) + Byte.valueOf(Integer.valueOf(Constant.DischargePowerLimitValue_set).byteValue()).byteValue()) + byteToInt(bBeginHour1.byteValue())) + byteToInt(bBeginMinute1.byteValue())) + byteToInt(bEndHour1.byteValue())) + byteToInt(bEndMinute1.byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), bBeginHour1.byteValue(), bBeginMinute1.byteValue(), bEndHour1.byteValue(), bEndMinute1.byteValue(), bDischargePowerRate.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.BackflowPrevention_REQUEST_PACKAGE /*57*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.BackflowPrevention_REQUEST_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.BackflowPrevention_REQUEST_CODE[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryMonth).byteValue());
//                Byte Backflow = Byte.valueOf((byte) 0);
//                if (Constant.BackflowPreventionState) {
//                    Backflow = Byte.valueOf((byte) 0);
//                } else if (!Constant.BackflowPreventionState) {
//                    Backflow = Byte.valueOf((byte) 1);
//                }
//                checkSum = intTo2Byte(Integer.valueOf(((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())) + byteToInt(Backflow.byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), Backflow.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.SetShadowScan_REQUEST_PACKAGE /*60*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.REQUEST_SetShadowScan_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.REQUEST_SetShadowScan_CODE[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryDay).byteValue());
//                Byte bModeh1 = Byte.valueOf(Byte.MIN_VALUE);
//                Byte bModew1 = Byte.valueOf((byte) 0);
//                if (Constant.ShadowscanPreventionState) {
//                    bModew1 = Byte.valueOf(Byte.MIN_VALUE);
//                } else if (!Constant.ShadowscanPreventionState) {
//                    bModew1 = Byte.valueOf((byte) 0);
//                }
//                checkSum = intTo2Byte(Integer.valueOf((((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())) + byteToInt(bModeh1.byteValue())) + byteToInt(bModew1.byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), bModeh1.byteValue(), bModew1.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.Setoffcharge_REQUEST_PACKAGE /*62*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.REQUEST_Setoffcharge_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.REQUEST_Setoffcharge_CODE[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryDay).byteValue());
//                Constant.BackupOrOffchargeFlag = flagsetworkmode;
//                Byte bModeh2 = Byte.valueOf((byte) 0);
//                Byte bModew2 = Byte.valueOf((byte) 0);
//                if (Constant.bRelayControlLow) {
//                    if (Constant.SelectoffchargeControl == flagsettime) {
//                        bModew2 = Byte.valueOf((byte) 48);
//                    } else if (Constant.SelectoffchargeControl == 0) {
//                        bModew2 = Byte.valueOf((byte) 32);
//                    }
//                } else if (Constant.SelectoffchargeControl == flagsettime) {
//                    bModew2 = Byte.valueOf((byte) 16);
//                } else if (Constant.SelectoffchargeControl == 0) {
//                    bModew2 = Byte.valueOf((byte) 0);
//                }
//                checkSum = intTo2Byte(Integer.valueOf((((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())) + byteToInt(bModeh2.byteValue())) + byteToInt(bModew2.byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), bModeh2.byteValue(), bModew2.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.LimitPower_GridUp_REQUEST_PACKAGE /*66*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.LimitPower_REQUEST_CODE_Grid_UP[flagcollect]);
//                fcode = Byte.valueOf(Constant.LimitPower_REQUEST_CODE_Grid_UP[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryDay).byteValue());
//                byte[] value = new byte[flagsetworkmode];
//                value = intTo2Byte(Constant.GridUp_limitPower);
//                checkSum = intTo2Byte(Integer.valueOf((((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())) + byteToInt(value[flagcollect])) + byteToInt(value[flagsettime])).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), value[flagcollect], value[flagsettime], checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.SAFTY_REQUEST_PACKAGE /*99*/:
//                checkSum = intTo2Byte(Integer.valueOf(((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue()).byteValue())) + byteToInt(Byte.valueOf(Constant.SAFTY_REQUEST_CODE[flagcollect]).byteValue())) + byteToInt(Byte.valueOf(Constant.SAFTY_REQUEST_CODE[flagsettime]).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryMonth).byteValue()).byteValue())) + byteToInt(Byte.valueOf((byte) Constant.SaftyCountryIndex).byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), bSaftyCountry.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.LeadAcid_REQUEST_PACKAGE /*101*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.LeadAcid_REQUEST_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.LeadAcid_REQUEST_CODE[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf("4").byteValue());
//                Constant.sendType = flagsettime;
//                Byte bModel = Byte.valueOf((byte) 1);
//                byte[] test = intTo2Byte(Constant.CapacityValue_set);
//                checkSum = intTo2Byte(Integer.valueOf((((((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())) + byteToInt(bModel.byteValue())) + byteToInt(Byte.valueOf((byte) 1).byteValue())) + byteToInt(test[flagcollect])) + byteToInt(test[flagsettime])).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), bModel.byteValue(), test[flagcollect], test[flagsettime], bCount.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.GetSetting_REQUEST_PACKAGE /*103*/:
//                checkSum = intTo2Byte(Integer.valueOf((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue()).byteValue())) + byteToInt(Byte.valueOf(Constant.REQUEST_SET_DATA_CODE[flagcollect]).byteValue())) + byteToInt(Byte.valueOf(Constant.REQUEST_SET_DATA_CODE[flagsettime]).byteValue())) + byteToInt(Byte.valueOf(Integer.valueOf(Constant.btn_stationdetail_power_QueryYear).byteValue()).byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.Lead_Charge_V_I_REQUEST_PACKAGE /*105*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.SET_BATTERY_CHARGE_PAREMETERS_REQUEST_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.SET_BATTERY_CHARGE_PAREMETERS_REQUEST_CODE[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf("4").byteValue());
//                byte[] ChargeV = intTo2Byte((int) Constant.Charge_V_Value_set);
//                byte[] ChargeI = intTo2Byte((int) Constant.Charge_I_Value_set);
//                checkSum = intTo2Byte(Integer.valueOf((((((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())) + byteToInt(ChargeV[flagcollect])) + byteToInt(ChargeV[flagsettime])) + byteToInt(ChargeI[flagcollect])) + byteToInt(ChargeI[flagsettime])).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), ChargeV[flagcollect], ChargeV[flagsettime], ChargeI[flagcollect], ChargeI[flagsettime], checkSum[flagcollect], checkSum[flagsettime]};
//            case Constant.Lead_Discharge_V_I_REQUEST_PACKAGE /*107*/:
//                dest = Byte.valueOf(Constant.INVENTER_DEFAULTSN.byteValue());
//                ccode = Byte.valueOf(Constant.SET_BATTERY_DISCHARGE_PAREMETERS_REQUEST_CODE[flagcollect]);
//                fcode = Byte.valueOf(Constant.SET_BATTERY_DISCHARGE_PAREMETERS_REQUEST_CODE[flagsettime]);
//                dataLength = Byte.valueOf(Integer.valueOf("5").byteValue());
//                byte[] DischargeV = intTo2Byte((int) Constant.Discharge_V_Value_set);
//                byte[] DischargeI = intTo2Byte((int) Constant.Discharge_I_Value_set);
//                checkSum = intTo2Byte(Integer.valueOf(((((((((((byteToInt(hHead.byteValue()) + byteToInt(lHead.byteValue())) + byteToInt(source.byteValue())) + byteToInt(dest.byteValue())) + byteToInt(ccode.byteValue())) + byteToInt(fcode.byteValue())) + byteToInt(dataLength.byteValue())) + byteToInt(DischargeV[flagcollect])) + byteToInt(DischargeV[flagsettime])) + byteToInt(DischargeI[flagcollect])) + byteToInt(DischargeI[flagsettime])) + byteToInt(Integer.valueOf(100 - Constant.Depth_Discharge_Value_set).byteValue())).intValue());
//                return new byte[]{hHead.byteValue(), lHead.byteValue(), source.byteValue(), dest.byteValue(), ccode.byteValue(), fcode.byteValue(), dataLength.byteValue(), DischargeV[flagcollect], DischargeV[flagsettime], DischargeI[flagcollect], DischargeI[flagsettime], depth_discharge, checkSum[flagcollect], checkSum[flagsettime]};
//            default:
//                return null;
//        }
//    }

//    public byte[] sendForData(byte[] in) throws Exception {
//        byte[] send;
//        synchronized (dclock) {
//            send = udp.send(in);
//        }
//        return send;
//    }

//    private byte[] fetchPackage(byte[] buf) {
//        byte[] pkg = new byte[flagcollect];
//        if (buf.length <= flagsetworkmode) {
//            return pkg;
//        }
//        int i = flagsettime;
//        while (i < buf.length) {
//            if (byteToInt(buf[i - 1]) == 170 && byteToInt(buf[i]) == 85 && buf.length > i + 7) {
//                byte[] bArr;
//                pkg = mergeByte(pkg, new byte[]{buf[i - 1], buf[i], buf[i + flagsettime], buf[i + flagsetworkmode], buf[i + 3], buf[i + 4], buf[i + 5]});
//                int length = byteToInt(buf[i + 5]);
//                i += 6;
//                int j = flagcollect;
//                while (j < length && i < buf.length) {
//                    bArr = new byte[flagsettime];
//                    bArr[flagcollect] = buf[i];
//                    pkg = mergeByte(pkg, bArr);
//                    j += flagsettime;
//                    i += flagsettime;
//                }
//                bArr = new byte[flagsetworkmode];
//                bArr[flagcollect] = buf[i];
//                bArr[flagsettime] = buf[i + flagsettime];
//                return mergeByte(pkg, bArr);
//            }
//            i += flagsettime;
//        }
//        return pkg;
//    }

//    private boolean compareByte(byte[] a, byte[] b) {
//        if (a.length != b.length) {
//            return false;
//        }
//        for (int i = flagcollect; i < a.length; i += flagsettime) {
//            if (a[i] != b[i]) {
//                return false;
//            }
//        }
//        return true;
//    }

//    private int checkPackageType(byte[] data) {
//        if (data == null || data.length <= 8) {
//            return -1;
//        }
//        byte[] bArr = new byte[flagsetworkmode];
//        bArr[flagcollect] = data[data.length - 2];
//        bArr[flagsettime] = data[data.length - 1];
//        int checkSum = byte2ToInt(bArr);
//        int total = flagcollect;
//        for (int i = flagcollect; i < data.length - 2; i += flagsettime) {
//            total += byteToInt(data[i]);
//        }
//        if (total != checkSum) {
//            return -1;
//        }
//        byte[] code = new byte[flagsetworkmode];
//        code[flagcollect] = data[4];
//        code[flagsettime] = data[5];
//        if (compareByte(Constant.REGISTER_QUERY_CODE, code)) {
//            return flagcollect;
//        }
//        if (compareByte(Constant.REGISTER_REQUEST_CODE, code)) {
//            return flagsettime;
//        }
//        if (compareByte(Constant.REGISTER_ADDRESS_CODE, code)) {
//            return flagsetworkmode;
//        }
//        if (compareByte(Constant.REGISTER_CONFIRM_ADDRESS_CODE, code)) {
//            return 3;
//        }
//        if (compareByte(Constant.REQUEST_INDEX_CODE, code)) {
//            return 6;
//        }
//        if (compareByte(Constant.RESPONSE_INDEX_CODE, code)) {
//            return 7;
//        }
//        if (compareByte(Constant.REQUEST_DATA_CODE, code)) {
//            return 8;
//        }
//        if (compareByte(Constant.RESPONSE_DATA_CODE, code)) {
//            return 9;
//        }
//        if (compareByte(Constant.CANCEL_REQUEST_CODE, code)) {
//            return 4;
//        }
//        if (compareByte(Constant.CANCEL_CONFIRM_REQUEST_CODE, code)) {
//            return 5;
//        }
//        if (compareByte(Constant.SETTIME_REQUEST_CODE, code)) {
//            return 10;
//        }
//        if (compareByte(Constant.SETTIME_RESPONSE_CODE, code)) {
//            return 11;
//        }
//        if (compareByte(Constant.SWITCH_SCI_REQUEST_CODE, code)) {
//            return 12;
//        }
//        if (compareByte(Constant.SWITCH_SCI_RESPONSE_CODE, code)) {
//            return 13;
//        }
//        if (compareByte(Constant.RESPONSE_SetStoreEnergyMode_CODE, code)) {
//            return 48;
//        }
//        if (compareByte(Constant.RESPONSE_SetShadowScan_CODE, code)) {
//            return 61;
//        }
//        if (compareByte(Constant.RESPONSE_SetRelayControl_CODE, code)) {
//            if (Constant.BackupOrOffchargeFlag == flagsettime) {
//                return 52;
//            }
//            if (Constant.BackupOrOffchargeFlag == flagsetworkmode) {
//                return 63;
//            }
//            return -1;
//        } else if (compareByte(Constant.RESPONSE_IDInfo_CODE, code)) {
//            return 15;
//        } else {
//            if (compareByte(Constant.LimitPower_RESPONSE_CODE, code)) {
//                return 17;
//            }
//            if (compareByte(Constant.BMSCmd_REQUEST_CODE, code)) {
//                return 18;
//            }
//            if (compareByte(Constant.BMSCmd_RESPONSE_CODE, code)) {
//                return 19;
//            }
//            if (compareByte(Constant.BatteryInformation_REQUEST_CODE, code)) {
//                return 20;
//            }
//            if (compareByte(Constant.BatteryInformation_RESPONSE_CODE, code)) {
//                return 21;
//            }
//            if (compareByte(Constant.BMSReplyPacket_REQUEST_CODE, code)) {
//                return 22;
//            }
//            if (compareByte(Constant.BMSReplyPacket_RESPONSE_CODE, code)) {
//                return 23;
//            }
//            if (compareByte(Constant.LimitPower_RESPONSE_CODE_CHARGE, code)) {
//                return 54;
//            }
//            if (compareByte(Constant.LimitPower_RESPONSE_CODE_DISCHARGE, code)) {
//                return 56;
//            }
//            if (compareByte(Constant.SAFTY_RESPONSE_CODE, code)) {
//                return 100;
//            }
//            if (compareByte(Constant.LeadAcid_RESPONSE_CODE, code)) {
//                return Constant.LeadAcid_RESPONSE_PACKAGE;
//            }
//            if (compareByte(Constant.BackflowPrevention_RESPONSE_CODE, code)) {
//                return 58;
//            }
//            if (compareByte(Constant.RESPONSE_SET_DATA_CODE, code)) {
//                return Constant.GetSetting_RESPONSE_PACKAGE;
//            }
//            if (compareByte(Constant.SET_BATTERY_CHARGE_PAREMETERS_RESPONSE_CODE, code)) {
//                return Constant.Lead_Charge_V_I_RESPONSE_PACKAGE;
//            }
//            if (compareByte(Constant.SET_BATTERY_DISCHARGE_PAREMETERS_RESPONSE_CODE, code)) {
//                return Constant.Lead_Discharge_V_I_RESPONSE_PACKAGE;
//            }
//            if (compareByte(Constant.LimitPower_RESPONSE_CODE_Grid_UP, code)) {
//                return 67;
//            }
//            return -1;
//        }
//    }

//    private boolean indexDataIsSend() {
//        Cursor cur = DbHepler.getDbImpl(context).getWritableDatabase().rawQuery("select count(*) from TinventerDataIndex where flag=0", null);
//        try {
//            if (!cur.moveToFirst() || cur.getInt(flagcollect) <= 0) {
//                return true;
//            }
//            return false;
//        } catch (Exception e) {
//            Log.e(TAG, "\u5224\u65ad\u7d22\u5f15\u5305\u662f\u5426\u53d1\u9001\u65f6\u5f02\u5e38:" + e.toString());
//            return true;
//        }
//    }

//    private byte[] fetchData(byte[] pkg) {
//        byte[] data = null;
//        if (pkg != null && pkg.length > 8) {
//            int dataLength = byteToInt(pkg[6]);
//            if (pkg.length == dataLength + 9) {
//                data = new byte[dataLength];
//                for (int i = flagcollect; i < dataLength; i += flagsettime) {
//                    data[i] = pkg[i + 7];
//                }
//            }
//            if (dataLength == 83) {
//                Constant.VersionFlag = flagsettime;
//            } else if (dataLength == 81) {
//                Constant.VersionFlag = flagcollect;
//            }
//        }
//        return data;
//    }

//    private String getWorkMode(int code) {
//        switch (code) {
//            case flagcollect /*0*/:
//                return this.res.getString(R.string.workmode_00);
//            case flagsettime /*1*/:
//                return this.res.getString(R.string.workmode_01);
//            case flagsetworkmode /*2*/:
//                return this.res.getString(R.string.workmode_02);
//            case StoreEnergyMode.Mode_wait /*3*/:
//                return this.res.getString(R.string.workmode_03);
//            default:
//                return null;
//        }
//    }

//    private String getPVMode(int code) {
//        switch (code) {
//            case flagcollect /*0*/:
//                return this.res.getString(R.string.pvmode_00);
//            case flagsettime /*1*/:
//                return this.res.getString(R.string.pvmode_01);
//            case flagsetworkmode /*2*/:
//                return this.res.getString(R.string.pvmode_02);
//            default:
//                return null;
//        }
//    }

//    private String getBatteryMode(int code) {
//        switch (code) {
//            case flagcollect /*0*/:
//                return this.res.getString(R.string.batterymode_00);
//            case flagsettime /*1*/:
//                return this.res.getString(R.string.batterymode_01);
//            case flagsetworkmode /*2*/:
//                return this.res.getString(R.string.batterymode_02);
//            case StoreEnergyMode.Mode_wait /*3*/:
//                return this.res.getString(R.string.batterymode_03);
//            case InverterMode.InverterMode_Mode4 /*4*/:
//                return this.res.getString(R.string.batterymode_04);
//            case InverterMode.InverterMode_Mode5 /*5*/:
//                return this.res.getString(R.string.batterymode_05);
//            default:
//                return null;
//        }
//    }

//    private String getLoadMode(int code) {
//        switch (code) {
//            case flagcollect /*0*/:
//                return this.res.getString(R.string.loadmode_00);
//            case flagsettime /*1*/:
//                return this.res.getString(R.string.loadmode_01);
//            default:
//                return null;
//        }
//    }

//    private String getEffectiveWorkMode(int code) {
//        switch (code) {
//            case flagcollect /*0*/:
//                return this.res.getString(R.string.effectivemode_00);
//            case flagsettime /*1*/:
//                return this.res.getString(R.string.effectivemode_01);
//            case flagsetworkmode /*2*/:
//                return this.res.getString(R.string.effectivemode_02);
//            case InverterMode.InverterMode_Mode4 /*4*/:
//                return this.res.getString(R.string.effectivemode_04);
//            case InverterMode.InverterMode_Mode8 /*8*/:
//                return this.res.getString(R.string.effectivemode_08);
//            case Constant.LimitPower_REQUEST_PACKAGE /*16*/:
//                return this.res.getString(R.string.effectivemode_10);
//            case Constant.EDAY_INDEX /*32*/:
//                return this.res.getString(R.string.effectivemode_20);
//            default:
//                return null;
//        }
//    }

//    private String getEffectiveRelayControlMode(int code) {
//        return null;
//    }

//    private String getGridInOutFlagMode(int code) {
//        switch (code) {
//            case flagcollect /*0*/:
//                return this.res.getString(R.string.gridinoutflagmode_00);
//            case flagsettime /*1*/:
//                return this.res.getString(R.string.gridinoutflagmode_01);
//            case flagsetworkmode /*2*/:
//                return this.res.getString(R.string.gridinoutflagmode_02);
//            default:
//                return null;
//        }
//    }

//    private String getStoreEnergyMode(int code) {
//        switch (code) {
//            case flagsettime /*1*/:
//                return this.res.getString(R.string.storeenergymode_01);
//            case flagsetworkmode /*2*/:
//                return this.res.getString(R.string.storeenergymode_02);
//            case InverterMode.InverterMode_Mode4 /*4*/:
//                return this.res.getString(R.string.storeenergymode_04);
//            case InverterMode.InverterMode_Mode8 /*8*/:
//                return this.res.getString(R.string.storeenergymode_08);
//            case Constant.LimitPower_REQUEST_PACKAGE /*16*/:
//                return this.res.getString(R.string.storeenergymode_10);
//            default:
//                return null;
//        }
//    }

//    private String getErrorMessage_BMS(int index) {
//        switch (index) {
//            case flagcollect /*0*/:
//                return this.res.getString(R.string.errormsg_00_BMS);
//            case flagsettime /*1*/:
//                return this.res.getString(R.string.errormsg_01_BMS);
//            case flagsetworkmode /*2*/:
//                return this.res.getString(R.string.errormsg_02_BMS);
//            case StoreEnergyMode.Mode_wait /*3*/:
//                return this.res.getString(R.string.errormsg_03_BMS);
//            case InverterMode.InverterMode_Mode4 /*4*/:
//                return this.res.getString(R.string.errormsg_04_BMS);
//            case InverterMode.InverterMode_Mode5 /*5*/:
//                return this.res.getString(R.string.errormsg_05_BMS);
//            case InverterMode.InverterMode_Mode6 /*6*/:
//                return this.res.getString(R.string.errormsg_06_BMS);
//            case InverterMode.InverterMode_Mode7 /*7*/:
//                return this.res.getString(R.string.errormsg_07_BMS);
//            case InverterMode.InverterMode_Mode8 /*8*/:
//                return this.res.getString(R.string.errormsg_08_BMS);
//            case InverterMode.InverterMode_Mode9 /*9*/:
//                return this.res.getString(R.string.errormsg_09_BMS);
//            default:
//                return null;
//        }
//    }

//    private String getErrorMessage(int index) {
//        switch (index) {
//            case flagcollect /*0*/:
//                return this.res.getString(R.string.errormsg_00);
//            case flagsettime /*1*/:
//                return this.res.getString(R.string.errormsg_01);
//            case flagsetworkmode /*2*/:
//                return this.res.getString(R.string.errormsg_02);
//            case StoreEnergyMode.Mode_wait /*3*/:
//                return this.res.getString(R.string.errormsg_03);
//            case InverterMode.InverterMode_Mode4 /*4*/:
//                return this.res.getString(R.string.errormsg_04);
//            case InverterMode.InverterMode_Mode5 /*5*/:
//                return this.res.getString(R.string.errormsg_05);
//            case InverterMode.InverterMode_Mode6 /*6*/:
//                return this.res.getString(R.string.errormsg_06);
//            case InverterMode.InverterMode_Mode7 /*7*/:
//                return this.res.getString(R.string.errormsg_07);
//            case InverterMode.InverterMode_Mode8 /*8*/:
//                return this.res.getString(R.string.errormsg_08);
//            case InverterMode.InverterMode_Mode9 /*9*/:
//                return this.res.getString(R.string.errormsg_09);
//            case InverterMode.InverterMode_Mode10 /*10*/:
//                return this.res.getString(R.string.errormsg_10);
//            case InverterMode.InverterMode_Mode11 /*11*/:
//                return this.res.getString(R.string.errormsg_11);
//            case InverterMode.InverterMode_Mode12 /*12*/:
//                return this.res.getString(R.string.errormsg_12);
//            case InverterMode.InverterMode_Mode13 /*13*/:
//                return this.res.getString(R.string.errormsg_13);
//            case InverterMode.InverterMode_Mode14 /*14*/:
//                return this.res.getString(R.string.errormsg_14);
//            case Constant.IDInfo_RESPONSE_PACKAGE /*15*/:
//                return this.res.getString(R.string.errormsg_15);
//            case Constant.LimitPower_REQUEST_PACKAGE /*16*/:
//                return this.res.getString(R.string.errormsg_16);
//            case Constant.LimitPower_RESPONSE_PACKAGE /*17*/:
//                return this.res.getString(R.string.errormsg_17);
//            case Constant.ETOTAL_H_INDEX /*18*/:
//                return this.res.getString(R.string.errormsg_18);
//            case Constant.ETOTAL_L_INDEX /*19*/:
//                return this.res.getString(R.string.errormsg_19);
//            case Constant.HTOTAL_H_INDEX /*20*/:
//                return this.res.getString(R.string.errormsg_20);
//            case Constant.HTOTAL_L_INDEX /*21*/:
//                return this.res.getString(R.string.errormsg_21);
//            case Constant.BMSReplyPacket_REQUEST_PACKAGE /*22*/:
//                return this.res.getString(R.string.errormsg_22);
//            case Constant.BMSReplyPacket_RESPONSE_PACKAGE /*23*/:
//                return this.res.getString(R.string.errormsg_23);
//            case Constant.SWITCH_SCI_REQUEST_PACKAGE1 /*24*/:
//                return this.res.getString(R.string.errormsg_24);
//            case Constant.SWITCH_SCI_RESPONSE_PACKAGE1 /*25*/:
//                return this.res.getString(R.string.errormsg_25);
//            case 26:
//                return this.res.getString(R.string.errormsg_26);
//            case 27:
//                return this.res.getString(R.string.errormsg_27);
//            case 28:
//                return this.res.getString(R.string.errormsg_28);
//            case 29:
//                return this.res.getString(R.string.errormsg_29);
//            case Constant.BATTERY_MIN /*30*/:
//                return this.res.getString(R.string.errormsg_30);
//            case 31:
//                return this.res.getString(R.string.errormsg_31);
//            default:
//                return null;
//        }
//    }

//    private String getOperatorMode(int value) {
//        switch (value) {
//            case flagcollect /*0*/:
//                return this.res.getString(R.string.operatormode_jp_00);
//            case flagsettime /*1*/:
//                return this.res.getString(R.string.operatormode_jp_01);
//            case flagsetworkmode /*2*/:
//                return this.res.getString(R.string.operatormode_jp_02);
//            default:
//                return this.res.getString(R.string.tv_Settings_RunStatusUnkonwn);
//        }
//    }

//    private byte[] getIndexPackage() throws Exception {
//        Log.d(TAG, "\u5f00\u59cb\u83b7\u53d6\u7d22\u5f15\u5305");
//        byte[] index = sendForData(buildPackage(6));
//        Log.d(TAG, "\u83b7\u53d6\u7d22\u5f15\u5305\u7ed3\u675f");
//        return index;
//    }

//    private byte[] getDataPackage() throws Exception {
//        byte[] data;
//        int i = flagcollect;
//        synchronized (sendlock) {
//            Log.d(TAG, "\u5f00\u59cb\u83b7\u53d6\u6570\u636e\u5305");
//            data = sendForData(buildPackage(8));
//            Log.d(TAG, "--\u5f00\u59cb\u83b7\u53d6\u6570\u636e\u5305\u957f\u5ea6\uff1a" + (data == null ? flagcollect : data.length));
//            StringBuilder append = new StringBuilder(String.valueOf(sdf.format(new Date(System.currentTimeMillis())))).append("-\u5f00\u59cb\u83b7\u53d6\u6570\u636e\u5305\u957f\u5ea6\uff1a");
//            if (data != null) {
//                i = data.length;
//            }
//            MainApplication.WriterLog(append.append(i).append('\n').toString());
//        }
//        return data;
//    }

//    private boolean saveInventerData(byte[] data) throws Exception {
//        return saveData(fetchData(data));
//    }

//    private boolean saveIndex(byte[] indexData) throws Exception {
//        boolean flag;
//        SQLiteDatabase db = DbHepler.getDbImpl(context).getWritableDatabase();
//        String sqlStr = "select count(*) from TinventerDataIndex ";
//        db.beginTransaction();
//        Cursor cur = db.rawQuery(sqlStr, null);
//        try {
//            if (!cur.moveToFirst() || cur.getInt(flagcollect) <= 0) {
//                flag = true;
//            } else {
//                flag = false;
//            }
//        } catch (Exception e) {
//            flag = true;
//        }
//        if (flag) {
//            int i = flagcollect;
//            while (i < indexData.length) {
//                try {
//                    db.execSQL("insert into TinventerDataIndex(_id,inventerSN,indexNo,indexSeq,indexName,flag) values('" + UUID.randomUUID().toString() + "'," + " '" + this.INVENTER_SN + "', " + indexData[i] + ", " + i + ", '" + getColomnName(indexData[i]) + "',0)");
//                    i += flagsettime;
//                } catch (Exception e2) {
//                    throw e2;
//                } catch (Throwable th) {
//                    db.endTransaction();
//                    db.close();
//                }
//            }
//            db.setTransactionSuccessful();
//        }
//        db.endTransaction();
//        db.close();
//        return true;
//    }

//    private boolean saveData(byte[] dataData) throws Exception {
//        try {
//            int i;
//            TinventerDataSource tds = new TinventerDataSource();
//            tds.setCreationDate(StringUtil.GetSysNow());
//            tds.setId(UUID.randomUUID().toString());
//            tds.setInventerSN(Constant.Inverter_sn);
//            byte[] bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[flagcollect];
//            bArr[flagsettime] = dataData[flagsettime];
//            Constant.REL_vpv1 = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setvpv1(Double.valueOf(Constant.REL_vpv1));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[flagsetworkmode];
//            bArr[flagsettime] = dataData[3];
//            Constant.REL_ipv1 = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setipv1(Double.valueOf(Constant.REL_ipv1));
//            Constant.REL_pv1Mode = getPVMode(byteToInt(dataData[4]));
//            tds.setpv1Mode(Constant.REL_pv1Mode);
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[5];
//            bArr[flagsettime] = dataData[6];
//            Constant.REL_vpv2 = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setvpv2(Double.valueOf(Constant.REL_vpv2));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[7];
//            bArr[flagsettime] = dataData[8];
//            Constant.REL_ipv2 = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setipv2(Double.valueOf(Constant.REL_ipv2));
//            Constant.REL_PVPower_int = (int) ((Constant.REL_vpv1 * Constant.REL_ipv1) + (Constant.REL_vpv2 * Constant.REL_ipv2));
//            Constant.REL_pv2Mode = getPVMode(byteToInt(dataData[9]));
//            tds.setpv2Mode(Constant.REL_pv2Mode);
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[10];
//            bArr[flagsettime] = dataData[11];
//            Constant.REL_vbattery1 = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setvbattery1(Double.valueOf(Constant.REL_vbattery1));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[12];
//            bArr[flagsettime] = dataData[13];
//            Constant.REL_vbattery2 = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setvbattery2(Double.valueOf(Constant.REL_vbattery2));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[14];
//            bArr[flagsettime] = dataData[15];
//            Constant.REL_vbattery3 = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setvbattery3(Double.valueOf(Constant.REL_vbattery3));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[16];
//            bArr[flagsettime] = dataData[17];
//            Constant.REL_vbattery4 = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setvbattery4(Double.valueOf(Constant.REL_vbattery4));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[18];
//            bArr[flagsettime] = dataData[19];
//            Constant.REL_ibattery1 = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setibattery1(Double.valueOf(Constant.REL_ibattery1));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[20];
//            bArr[flagsettime] = dataData[21];
//            Constant.REL_ibattery2 = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setibattery2(Double.valueOf(Constant.REL_ibattery2));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[22];
//            bArr[flagsettime] = dataData[23];
//            Constant.REL_ibattery3 = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setibattery3(Double.valueOf(Constant.REL_ibattery3));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[24];
//            bArr[flagsettime] = dataData[25];
//            Constant.REL_ibattery4 = (double) byte2ToInt(bArr);
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[25];
//            bArr[flagsettime] = dataData[24];
//            int[] bits_BMS = byteTobit(bArr);
//            String message_BMS = StringUtil.EMPTY;
//            for (i = flagcollect; i < bits_BMS.length; i += flagsettime) {
//                if (bits_BMS[i] == flagsettime) {
//                    message_BMS = new StringBuilder(String.valueOf(message_BMS)).append(getErrorMessage_BMS(i)).append(",").toString();
//                }
//            }
//            if (message_BMS.length() > 0) {
//                message_BMS = message_BMS.substring(flagcollect, message_BMS.length() - 1);
//            } else {
//                message_BMS = "Battery Communication OK";
//            }
//            Constant.REL_batteryBMS = message_BMS;
//            tds.setibattery4(new StringBuilder(String.valueOf(StringUtil.FormatDouble(Double.valueOf(Constant.REL_ibattery4)))).append(Constant.REL_batteryBMS).toString());
//            Constant.REL_cbattery1 = (double) byteToInt(dataData[26]);
//            tds.setcbattery1(Double.valueOf(Constant.REL_cbattery1));
//            Constant.REL_cbattery2 = (double) byteToInt(dataData[27]);
//            tds.setcbattery2(Double.valueOf(Constant.REL_cbattery2));
//            Constant.REL_cbattery3 = (double) byteToInt(dataData[28]);
//            tds.setcbattery3(Double.valueOf(Constant.REL_cbattery3));
//            Constant.REL_cbattery4 = (double) byteToInt(dataData[29]);
//            tds.setcbattery4(Double.valueOf(Constant.REL_cbattery4));
//            int battery1mode = byteToInt(dataData[30]);
//            Constant.REL_battery1Mode = getBatteryMode(battery1mode);
//            tds.setbattery1Mode(Constant.REL_battery1Mode);
//            Constant.REL_battery1Mode_int = battery1mode;
//            Constant.REL_battery2Mode = getBatteryMode(byteToInt(dataData[31]));
//            tds.setbattery2Mode(Constant.REL_battery2Mode);
//            Constant.REL_battery3Mode = getBatteryMode(byteToInt(dataData[32]));
//            tds.setbattery3Mode(Constant.REL_battery3Mode);
//            Constant.REL_battery4Mode = getBatteryMode(byteToInt(dataData[33]));
//            tds.setbattery4Mode(Constant.REL_battery4Mode);
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[34];
//            bArr[flagsettime] = dataData[35];
//            Constant.REL_vgrid = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setvgrid(Double.valueOf(Constant.REL_vgrid));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[36];
//            bArr[flagsettime] = dataData[37];
//            Constant.REL_igrid = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setigrid(Double.valueOf(Constant.REL_igrid));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[38];
//            bArr[flagsettime] = dataData[39];
//            Constant.REL_pgrid = (double) byte2ToInt(bArr);
//            tds.setpgrid(Double.valueOf(Constant.REL_pgrid));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[40];
//            bArr[flagsettime] = dataData[41];
//            Constant.REL_fgrid = ((double) byte2ToInt(bArr)) * 0.01d;
//            tds.setfgrid(Double.valueOf(Constant.REL_fgrid));
//            Constant.REL_gridMode = getWorkMode(byteToInt(dataData[42]));
//            tds.setgridMode(Constant.REL_gridMode);
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[43];
//            bArr[flagsettime] = dataData[44];
//            Constant.REL_vload = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setvload(Double.valueOf(Constant.REL_vload));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[45];
//            bArr[flagsettime] = dataData[46];
//            Constant.REL_iload = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.setiload(Double.valueOf(Constant.REL_iload));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[47];
//            bArr[flagsettime] = dataData[48];
//            Constant.REL_pload = (double) byte2ToInt(bArr);
//            tds.setpload(Double.valueOf(Constant.REL_pload));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[49];
//            bArr[flagsettime] = dataData[50];
//            Constant.REL_fload = ((double) byte2ToInt(bArr)) * 0.01d;
//            tds.setfload(Double.valueOf(Constant.REL_fload));
//            int loadmode = byteToInt(dataData[51]);
//            Constant.REL_loadModeVlaue = loadmode;
//            Constant.REL_loadMode = getLoadMode(loadmode);
//            tds.setloadMode(Constant.REL_loadMode);
//            int storeenergymode = byteToInt(dataData[52]);
//            Constant.REL_workMode = storeenergymode;
//            Constant.REL_strworkMode = getStoreEnergyMode(storeenergymode);
//            tds.setWorkMode(Constant.REL_strworkMode);
//            tds.iworkMode = storeenergymode;
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[53];
//            bArr[flagsettime] = dataData[54];
//            Constant.REL_temperature = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.settemperature(Double.valueOf(Constant.REL_temperature));
//            int iErrorMessage = byte4ToInt(new byte[]{dataData[55], dataData[56], dataData[57], dataData[58]});
//            int[] bits = byteTobit(new byte[]{dataData[58], dataData[57], dataData[56], dataData[55]});
//            String message = StringUtil.EMPTY;
//            if (iErrorMessage != Constant.iErrorMessage) {
//                Constant.iErrorMessage = iErrorMessage;
//                for (i = flagcollect; i < bits.length; i += flagsettime) {
//                    if (bits[i] == flagsettime) {
//                        message = new StringBuilder(String.valueOf(message)).append(getErrorMessage(i)).append(",").toString();
//                    }
//                }
//            }
//            if (message.length() > 0) {
//                message = message.substring(flagcollect, message.length() - 1);
//                HashMap<String, String> map;
//                if (Constant.listItem.size() < 50) {
//                    map = new HashMap();
//                    map.put("time", StringUtil.GetSysNow());
//                    map.put("errormessage", message);
//                    Constant.listItem.add(map);
//                } else {
//                    Constant.listItem.remove(flagcollect);
//                    map = new HashMap();
//                    map.put("time", StringUtil.GetSysNow());
//                    map.put("errormessage", message);
//                    Constant.listItem.add(map);
//                }
//                DbHepler.getDbImpl(context).execSQL("insert into TinverterError(inventerSN,createTime,errorMessage) values('" + Constant.Inverter_sn + "','" + StringUtil.GetSysNow() + "','" + message + "' )");
//            }
//            Constant.REL_errorMessage = message;
//            tds.setErrorMessage(message);
//            Constant.REL_eTotal = ((double) byte4ToInt(new byte[]{dataData[59], dataData[60], dataData[61], dataData[62]})) * 0.1d;
//            tds.seteTotal(Double.valueOf(Constant.REL_eTotal));
//            Constant.REL_hTotal = (double) byte4ToInt(new byte[]{dataData[63], dataData[64], dataData[65], dataData[66]});
//            tds.sethTotal(Double.valueOf(Constant.REL_hTotal));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[67];
//            bArr[flagsettime] = dataData[68];
//            Constant.REL_eDay = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.seteDay(Double.valueOf(Constant.REL_eDay));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[69];
//            bArr[flagsettime] = dataData[70];
//            Constant.REL_eLoadDay = ((double) byte2ToInt(bArr)) * 0.1d;
//            tds.seteLoadDay(Double.valueOf(Constant.REL_eLoadDay));
//            Constant.REL_eTotalLoad = ((double) byte4ToInt(new byte[]{dataData[71], dataData[72], dataData[73], dataData[74]})) * 0.1d;
//            tds.seteTotalLoad(Double.valueOf(Constant.REL_eTotalLoad));
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[75];
//            bArr[flagsettime] = dataData[76];
//            Constant.REL_totalPower = (double) byte2ToInt(bArr);
//            tds.settotalPower(Double.valueOf(Constant.REL_totalPower));
//            int effectiveworkmode = byteToInt(dataData[77]);
//            Constant.REL_int_effectiveWorkMode = effectiveworkmode;
//            Log.d(TAG, "---effectiveworkmode=====" + effectiveworkmode);
//            Constant.REL_effectiveWorkMode = getEffectiveWorkMode(effectiveworkmode);
//            tds.seteffectiveWorkMode(Constant.REL_effectiveWorkMode);
//            bArr = new byte[flagsetworkmode];
//            bArr[flagcollect] = dataData[78];
//            bArr[flagsettime] = dataData[79];
//            int effectiverelaycontrol = byte2ToInt(bArr);
//            Constant.REL_effectiveRelayControl = getEffectiveRelayControlMode(effectiverelaycontrol);
//            tds.seteffectiveRelayControl(Constant.REL_effectiveRelayControl);
//            Constant.REL_int_effectiveRelayControl = effectiverelaycontrol;
//            int gridinoutflag = byteToInt(dataData[80]);
//            Constant.REL_gridInOutFlag = getGridInOutFlagMode(gridinoutflag);
//            tds.setgridInOutFlag(Constant.REL_gridInOutFlag);
//            Constant.REL_gridInOutFlag_int = gridinoutflag;
//            if (flagsettime == Constant.VersionFlag) {
//                bArr = new byte[flagsetworkmode];
//                bArr[flagcollect] = dataData[81];
//                bArr[flagsettime] = dataData[82];
//                Constant.REL_pback_up = (double) byte2ToInt(bArr);
//            }
//            Constant.DbName = new File(Constant.LogPath + File.separator + DBsdf.format(new Date()) + ".db");
//            SQLiteDatabase database = SQLiteDatabase.openOrCreateDatabase(Constant.DbName, null);
//            database.execSQL("create table if not exists TinventerDataSource(_id text primary key, inventerSN text,vpv1 real,ipv1 real,pv1mode text,vpv2 real,ipv2 real,pv2mode text,vbattery1 real,vbattery2 real,vbattery3 real,vbattery4 real,ibattery1 real,ibattery2 real,ibattery3 real,ibattery4 text,cbattery1 real,cbattery2 real,cbattery3 real,cbattery4 real,battery1mode text,battery2mode text,battery3mode text,battery4mode text,vgrid real,igrid real,pgrid real,fgrid real,gridmode text,vload real,iload real,loadPower real,fload real,loadmode text,workMode text,temperature real,errorMessage text,eTotal real,hTotal real,eDay real,eLoadDay real,eTotalLoad  real,totalpower real,effectiveworkmode text,effectiveRelayControl text,gridinoutflag text,creationDate datetime )");
//            database.execSQL("insert into TinventerDataSource(_id, inventerSN, vpv1,ipv1,pv1mode,vpv2,ipv2,pv2mode,vbattery1,vbattery2,vbattery3,vbattery4,ibattery1,ibattery2,ibattery3,ibattery4,cbattery1,cbattery2,cbattery3,cbattery4,battery1mode,battery2mode,battery3mode,battery4mode,vgrid,igrid,pgrid,fgrid,gridmode,vload,iload,loadPower,fload,loadmode,workMode,temperature,errorMessage,eTotal,hTotal,eDay,eLoadDay,eTotalLoad,totalpower,effectiveworkmode,effectiveRelayControl,gridinoutflag,creationDate )  values('" + tds.getId() + "', '" + tds.getInventerSN() + "', '" + tds.getvpv1() + "', '" + tds.getipv1() + "','" + tds.getpv1Mode() + "','" + tds.getvpv2() + "', '" + tds.getipv2() + "','" + tds.getpv2Mode() + "','" + tds.getvbattery1() + "','" + tds.getvbattery2() + "','" + tds.getvbattery3() + "','" + tds.getvbattery4() + "','" + tds.getibattery1() + "','" + tds.getibattery2() + "','" + tds.getibattery3() + "','" + tds.getibattery4() + "','" + tds.getcbattery1() + "','" + tds.getcbattery2() + "','" + tds.getcbattery3() + "','" + tds.getcbattery4() + "','" + tds.getbattery1Mode() + "','" + tds.getbattery2Mode() + "','" + tds.getbattery3Mode() + "','" + tds.getbattery4Mode() + "','" + tds.getvgrid() + "','" + tds.getigrid() + "','" + tds.getpgrid() + "','" + tds.getfgrid() + "','" + tds.getgridMode() + "','" + tds.getvload() + "','" + tds.getiload() + "','" + tds.getpload() + "','" + tds.getfload() + "','" + tds.getloadMode() + "','" + tds.getWorkMode() + "','" + tds.gettemperature() + "','" + tds.getErrorMessage() + "','" + tds.geteTotal() + "','" + tds.gethTotal() + "','" + tds.geteDay() + "','" + tds.geteLoadDay() + "','" + tds.geteTotalLoad() + "','" + tds.gettotalPower() + "','" + tds.geteffectiveWorkMode() + "','" + tds.geteffectiveRelayControl() + "','" + tds.getgridInOutFlag() + "','" + tds.getCreationDate() + "' )");
//            database.close();
//            return true;
//        } catch (Exception e) {
//            Log.e("DataCollectUtil--saveData()", e.toString());
//            return false;
//        }
//    }

//    private boolean clearUpData() throws Exception {
//        DbHepler dbh = DbHepler.getDbImpl(context);
//        SQLiteDatabase db = dbh.getWritableDatabase();
//        db.beginTransaction();
//        try {
//            Cursor datetime = dbh.query("select max(strftime('%Y-%m-%d %H',creationDate)) from tinventerdatasource group by strftime('%Y-%m-%d %H',creationDate)", null);
//            while (datetime.moveToNext()) {
//                String creationdate = datetime.getString(flagcollect);
//                Cursor exist = dbh.query(" select 1 from tinventerdata where strftime('%Y-%m-%d %H',creationDate)= '" + creationdate + "'", null);
//                boolean flag = false;
//                if (exist.getCount() > 0 && exist.moveToFirst() && flagsettime == exist.getInt(flagcollect)) {
//                    flag = true;
//                    Cursor record = dbh.query(" select pgrid,loadPower,eDay,eLoadDay,eTotal,hTotal,eTotalLoad,creationDate, max(eDay)-min(eDay) eHour, max(eLoadDay)-min(eLoadDay) eLoadHour from tinventerdatasource where strftime('%Y-%m-%d %H',creationDate)= '" + creationdate + "'" + " group by strftime('%Y-%m-%d %H',creationDate)", null);
//                    if (record.moveToFirst()) {
//                        Double pac = Double.valueOf(record.getDouble(record.getColumnIndex(TinventerDataSource.COLNAME_PGRID)));
//                        Double loadPower = Double.valueOf(record.getDouble(record.getColumnIndex("loadPower")));
//                        Double eDay = Double.valueOf(record.getDouble(record.getColumnIndex(TinventerDataSource.COLNAME_EDAY)));
//                        Double eLoadDay = Double.valueOf(record.getDouble(record.getColumnIndex(TinventerDataSource.COLNAME_ELOADDAY)));
//                        Double eTotal = Double.valueOf(record.getDouble(record.getColumnIndex(TinventerDataSource.COLNAME_ETOTAL)));
//                        Double hTotal = Double.valueOf(record.getDouble(record.getColumnIndex(TinventerDataSource.COLNAME_HTOTAL)));
//                        Double eTotalLoad = Double.valueOf(record.getDouble(record.getColumnIndex(TinventerDataSource.COLNAME_ETOTALLOAD)));
//                        String creationDate = record.getString(record.getColumnIndex(TinventerDataSource.COLNAME_CREATIONDATE));
//                        Double ehour = Double.valueOf(record.getDouble(record.getColumnIndex("eHour")));
//                        dbh.execSQL(" update tinventerdata set loadPower=" + loadPower + ",eDay=" + eDay + ",eLoadDay=" + eLoadDay + ",eTotal=" + eTotal + ",hTotal=" + hTotal + ",eTotalLoad=" + eTotalLoad + ",creationDate='" + creationDate + "',eHour=" + ehour + ",eLoadHour=" + Double.valueOf(record.getDouble(record.getColumnIndex("eLoadHour"))) + "  where strftime('%Y-%m-%d %H',creationDate)= '" + creationdate + "'");
//                    }
//                }
//                if (!flag) {
//                    dbh.execSQL("insert into tinventerdata(_id,inventerSN,loadPower,eDay,eLoadDay,eTotal,hTotal,eTotalLoad,creationDate,eHour,eLoadHour) select  '" + UUID.randomUUID().toString() + "',inventerSN,loadPower,eDay,eLoadDay," + "eTotal,hTotal,eTotalLoad,creationDate,max(eDay)-min(eDay) ehour, max(eLoadDay)-min(eLoadDay) eloadhour" + " from tinventerdatasource where strftime('%Y-%m-%d %H',creationDate)= '" + creationdate + "'" + " group by strftime('%Y-%m-%d %H',creationDate)");
//                }
//                if (!datetime.isLast()) {
//                    dbh.execSQL(" delete from tinventerdatasource where strftime('%Y-%m-%d %H',creationDate)= '" + creationdate + "'");
//                }
//            }
//            db.setTransactionSuccessful();
//            db.endTransaction();
//            db.close();
//            return true;
//        } catch (Exception e) {
//            throw e;
//        } catch (Throwable th) {
//            db.endTransaction();
//            db.close();
//        }
//    }

//    private byte[] buildIndexPackage(byte[] index) {
//        byte[] data = fetchData(index);
//        byte[] bArr = new byte[flagcollect];
//        byte[] bArr2 = new byte[flagsettime];
//        bArr2[flagcollect] = Integer.valueOf(flagsettime).byteValue();
//        byte[] indexs = mergeByte(mergeByte(bArr, bArr2), Constant.MONITOR_SN);
//        int dataLengthPre = data.length + 19;
//        byte lbyte = Integer.valueOf(dataLengthPre & 255).byteValue();
//        bArr = new byte[flagsetworkmode];
//        bArr[flagcollect] = Integer.valueOf((dataLengthPre >> 8) & 255).byteValue();
//        bArr[flagsettime] = lbyte;
//        indexs = mergeByte(mergeByte(indexs, bArr), this.INVENTER_SN_BYTE);
//        bArr = new byte[flagsettime];
//        bArr[flagcollect] = Integer.valueOf(flagcollect).byteValue();
//        indexs = mergeByte(indexs, bArr);
//        int reldataLengthPre = data.length;
//        lbyte = Integer.valueOf(reldataLengthPre & 255).byteValue();
//        bArr = new byte[flagsetworkmode];
//        bArr[flagcollect] = Integer.valueOf((reldataLengthPre >> 8) & 255).byteValue();
//        bArr[flagsettime] = lbyte;
//        return mergeByte(mergeByte(indexs, bArr), data);
//    }

//    private byte[] buildDataPackage(byte[] data) {
//        byte[] dataData = fetchData(data);
//        byte[] bArr = new byte[flagcollect];
//        byte[] bArr2 = new byte[flagsettime];
//        bArr2[flagcollect] = Integer.valueOf(flagsettime).byteValue();
//        byte[] datas = mergeByte(mergeByte(bArr, bArr2), Constant.MONITOR_SN);
//        int dataLengthPre = dataData.length + 19;
//        byte lbyte = Integer.valueOf(dataLengthPre & 255).byteValue();
//        bArr = new byte[flagsetworkmode];
//        bArr[flagcollect] = Integer.valueOf((dataLengthPre >> 8) & 255).byteValue();
//        bArr[flagsettime] = lbyte;
//        datas = mergeByte(mergeByte(datas, bArr), this.INVENTER_SN_BYTE);
//        bArr = new byte[flagsettime];
//        bArr[flagcollect] = Integer.valueOf(flagsettime).byteValue();
//        datas = mergeByte(datas, bArr);
//        int reldataLengthPre = dataData.length;
//        lbyte = Integer.valueOf(reldataLengthPre & 255).byteValue();
//        bArr = new byte[flagsetworkmode];
//        bArr[flagcollect] = Integer.valueOf((reldataLengthPre >> 8) & 255).byteValue();
//        bArr[flagsettime] = lbyte;
//        return mergeByte(mergeByte(datas, bArr), dataData);
//    }

//    private boolean setInventerIndexFlag() {
//        return DbHepler.getDbImpl(context).execSQL("update TinventerDataIndex  set flag=1 where inventerSN='" + this.INVENTER_SN + "'");
//    }

//    private boolean canceled() throws Exception {
//        synchronized (cellock) {
//            for (int i = flagcollect; i < status.length; i += flagsettime) {
//                if (status[i] == flagsettime) {
//                    return false;
//                }
//            }
//            if (checkPackageType(sendForData(buildPackage(4))) == 5) {
//                return true;
//            }
//            return false;
//        }
//    }

//    private String getColomnName(int index) {
//        String colName = StringUtil.EMPTY;
//        switch (index) {
//            case InverterMode.InverterMode_Mode14 /*14*/:
//                return TinventerDataSource.COLNAME_WORKMODE;
//            case Constant.LimitPower_REQUEST_PACKAGE /*16*/:
//                return TinventerDataSource.COLNAME_ERRORMESSAGE;
//            case Constant.LimitPower_RESPONSE_PACKAGE /*17*/:
//                return TinventerDataSource.COLNAME_ERRORMESSAGE;
//            case Constant.ETOTAL_H_INDEX /*18*/:
//                return TinventerDataSource.COLNAME_ETOTAL;
//            case Constant.ETOTAL_L_INDEX /*19*/:
//                return TinventerDataSource.COLNAME_ETOTAL;
//            case Constant.HTOTAL_H_INDEX /*20*/:
//                return TinventerDataSource.COLNAME_HTOTAL;
//            case Constant.EDAY_INDEX /*32*/:
//                return TinventerDataSource.COLNAME_EDAY;
//            case Constant.ELOADDAY_INDEX /*40*/:
//                return TinventerDataSource.COLNAME_ELOADDAY;
//            case Constant.ETOTALLOAD_H_INDEX /*41*/:
//                return TinventerDataSource.COLNAME_ETOTALLOAD;
//            case Constant.ETOTALLOAD_L_INDEX /*42*/:
//                return TinventerDataSource.COLNAME_ETOTALLOAD;
//            default:
//                return colName;
//        }
//    }

//    private byte intToByte(int number) {
//        return new Integer(number & 255).byteValue();
//    }

//    private byte[] intTo2Byte(int number) {
//        int temp = number;
//        byte[] b = new byte[flagsetworkmode];
//        b[flagsettime] = new Integer(temp & 255).byteValue();
//        b[flagcollect] = new Integer((temp >> 8) & 255).byteValue();
//        return b;
//    }

//    private int byteToInt(byte b) {
//        return b & 255;
//    }

//    private int byte2ToInt(byte[] b) {
//        int s;
//        if (b[flagcollect] >= (byte) 0) {
//            s = flagcollect + b[flagcollect];
//        } else {
//            s = b[flagcollect] + 256;
//        }
//        s <<= 8;
//        if (b[flagsettime] >= (byte) 0) {
//            return s + b[flagsettime];
//        }
//        return (s + 256) + b[flagsettime];
//    }

//    private int byte4ToInt(byte[] b) {
//        int s = flagcollect;
//        for (int i = flagcollect; i < b.length - 1; i += flagsettime) {
//            if (b[i] >= (byte) 0) {
//                s += b[i];
//            } else {
//                s = (s + 256) + b[i];
//            }
//            s <<= 8;
//        }
//        if (b[3] >= (byte) 0) {
//            return s + b[3];
//        }
//        return (s + 256) + b[3];
//    }

//    private int[] byteTobit(byte[] bt) {
//        int[] bits = new int[(bt.length * 8)];
//        for (int j = flagcollect; j < bt.length; j += flagsettime) {
//            byte b = bt[j];
//            for (int i = flagcollect; i < 8; i += flagsettime) {
//                bits[(j * 8) + i] = (b >> i) & flagsettime;
//            }
//        }
//        return bits;
//    }

//    private byte[] mergeByte(byte[] pre, byte[] post) {
//        byte[] data = null;
//        if (!(pre == null || post == null)) {
//            data = new byte[(pre.length + post.length)];
//            for (int i = flagcollect; i < data.length; i += flagsettime) {
//                if (i < pre.length) {
//                    data[i] = pre[i];
//                } else {
//                    data[i] = post[i - pre.length];
//                }
//            }
//        }
//        return data;
//    }

//    public static boolean SetRelayControl() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(51));
//            if (dcUtil.checkPackageType(confirmData) != 52) {
//                return false;
//            }
//            byte[] data = dcUtil.fetchData(confirmData);
//            if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                return true;
//            }
//            return false;
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//            return false;
//        }
//    }

//    public static boolean GetIDInfo() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(14));
//            if (dcUtil.checkPackageType(confirmData) != 15) {
//                return false;
//            }
//            byte[] data = dcUtil.fetchData(confirmData);
//            if (data == null) {
//                return false;
//            }
//            byte[] data_fireware_version = new byte[5];
//            System.arraycopy(data, flagcollect, data_fireware_version, flagcollect, 5);
//            Constant.Inverter_fireware_version = new String(data_fireware_version);
//            byte[] data_model_name = new byte[10];
//            System.arraycopy(data, 5, data_model_name, flagcollect, 10);
//            Constant.Inverter_model_name = new String(data_model_name);
//            byte[] data_Manufacturer = new byte[16];
//            System.arraycopy(data, 15, data_Manufacturer, flagcollect, 16);
//            Constant.Inverter_manufacturer = new String(data_Manufacturer);
//            byte[] data_sn = new byte[16];
//            System.arraycopy(data, 31, data_sn, flagcollect, 16);
//            Constant.Inverter_sn = new String(data_sn);
//            if (!Constant.Inverter_sn.equals(Constant.InverterSN)) {
//                PropertyUtil.SetValue(context, "InverterSN", Constant.Inverter_sn);
//                Constant.InverterSN = Constant.Inverter_sn;
//                Constant.listItem.clear();
//            }
//            byte[] data_soft_version = new byte[12];
//            System.arraycopy(data, 51, data_soft_version, flagcollect, 12);
//            Constant.Inverter_software_version = new String(data_soft_version);
//            byte[] data_safty_country = new byte[flagsettime];
//            System.arraycopy(data, 63, data_safty_country, flagcollect, flagsettime);
//            switch (data_safty_country[flagcollect]) {
//                case flagcollect /*0*/:
//                    Constant.Inverter_safty_country = "ITALY";
//                    Constant.SaftyCountryIndex = flagcollect;
//                    Constant.SaftyCountryIndex_Show = 23;
//                    break;
//                case flagsettime /*1*/:
//                    Constant.Inverter_safty_country = "CZECH";
//                    Constant.SaftyCountryIndex = flagsettime;
//                    Constant.SaftyCountryIndex_Show = 10;
//                    break;
//                case flagsetworkmode /*2*/:
//                    Constant.Inverter_safty_country = "GERMANY";
//                    Constant.SaftyCountryIndex = flagsetworkmode;
//                    Constant.SaftyCountryIndex_Show = 17;
//                    break;
//                case StoreEnergyMode.Mode_wait /*3*/:
//                    Constant.Inverter_safty_country = "SPAIN";
//                    Constant.SaftyCountryIndex = 3;
//                    Constant.SaftyCountryIndex_Show = 30;
//                    break;
//                case InverterMode.InverterMode_Mode4 /*4*/:
//                    Constant.Inverter_safty_country = "GREECE MAINLAND";
//                    Constant.SaftyCountryIndex = 4;
//                    Constant.SaftyCountryIndex_Show = 18;
//                    break;
//                case InverterMode.InverterMode_Mode5 /*5*/:
//                    Constant.Inverter_safty_country = "DENMARK";
//                    Constant.SaftyCountryIndex = 5;
//                    Constant.SaftyCountryIndex_Show = 11;
//                    break;
//                case InverterMode.InverterMode_Mode6 /*6*/:
//                    Constant.Inverter_safty_country = "BELGIUM";
//                    Constant.SaftyCountryIndex = 6;
//                    Constant.SaftyCountryIndex_Show = 6;
//                    break;
//                case InverterMode.InverterMode_Mode7 /*7*/:
//                    Constant.Inverter_safty_country = "ROMANIA";
//                    Constant.SaftyCountryIndex = 7;
//                    Constant.SaftyCountryIndex_Show = 28;
//                    break;
//                case InverterMode.InverterMode_Mode8 /*8*/:
//                    Constant.Inverter_safty_country = "G83SPEC";
//                    Constant.SaftyCountryIndex = 8;
//                    Constant.SaftyCountryIndex_Show = 16;
//                    break;
//                case InverterMode.InverterMode_Mode9 /*9*/:
//                    Constant.Inverter_safty_country = "AUSTRALIAN";
//                    Constant.SaftyCountryIndex = 9;
//                    Constant.SaftyCountryIndex_Show = 4;
//                    break;
//                case InverterMode.InverterMode_Mode10 /*10*/:
//                    Constant.Inverter_safty_country = "FRANCE";
//                    Constant.SaftyCountryIndex = 10;
//                    Constant.SaftyCountryIndex_Show = 12;
//                    break;
//                case InverterMode.InverterMode_Mode11 /*11*/:
//                    Constant.Inverter_safty_country = "CHINA";
//                    Constant.SaftyCountryIndex = 11;
//                    Constant.SaftyCountryIndex_Show = 8;
//                    break;
//                case InverterMode.InverterMode_Mode12 /*12*/:
//                    Constant.Inverter_safty_country = "60Hz GRID DEFAULT";
//                    Constant.SaftyCountryIndex = 12;
//                    Constant.SaftyCountryIndex_Show = flagsettime;
//                    break;
//                case InverterMode.InverterMode_Mode13 /*13*/:
//                    Constant.Inverter_safty_country = "POLAND";
//                    Constant.SaftyCountryIndex = 13;
//                    Constant.SaftyCountryIndex_Show = 27;
//                    break;
//                case InverterMode.InverterMode_Mode14 /*14*/:
//                    Constant.Inverter_safty_country = "SOUTH AFRICA";
//                    Constant.SaftyCountryIndex = 14;
//                    Constant.SaftyCountryIndex_Show = 29;
//                    break;
//                case Constant.LimitPower_REQUEST_PACKAGE /*16*/:
//                    Constant.Inverter_safty_country = "BRAZIL";
//                    Constant.SaftyCountryIndex = 16;
//                    Constant.SaftyCountryIndex_Show = 7;
//                    break;
//                case Constant.LimitPower_RESPONSE_PACKAGE /*17*/:
//                    Constant.Inverter_safty_country = "THAILAND MEA";
//                    Constant.SaftyCountryIndex = 17;
//                    Constant.SaftyCountryIndex_Show = 31;
//                    break;
//                case Constant.ETOTAL_H_INDEX /*18*/:
//                    Constant.Inverter_safty_country = "THAILAND PEA";
//                    Constant.SaftyCountryIndex = 18;
//                    Constant.SaftyCountryIndex_Show = 32;
//                    break;
//                case Constant.ETOTAL_L_INDEX /*19*/:
//                    Constant.Inverter_safty_country = "MAURITIUS";
//                    Constant.SaftyCountryIndex = 19;
//                    Constant.SaftyCountryIndex_Show = 25;
//                    break;
//                case Constant.HTOTAL_H_INDEX /*20*/:
//                    Constant.Inverter_safty_country = "HOLLAND";
//                    Constant.SaftyCountryIndex = 20;
//                    Constant.SaftyCountryIndex_Show = 19;
//                    break;
//                case Constant.HTOTAL_L_INDEX /*21*/:
//                    Constant.Inverter_safty_country = "G59/3";
//                    Constant.SaftyCountryIndex = 21;
//                    Constant.SaftyCountryIndex_Show = 15;
//                    break;
//                case Constant.BMSReplyPacket_REQUEST_PACKAGE /*22*/:
//                    Constant.Inverter_safty_country = "CHINA SPECIAL";
//                    Constant.SaftyCountryIndex = 22;
//                    Constant.SaftyCountryIndex_Show = 9;
//                    break;
//                case Constant.BMSReplyPacket_RESPONSE_PACKAGE /*23*/:
//                    Constant.Inverter_safty_country = "FRENCH 50Hz";
//                    Constant.SaftyCountryIndex = 23;
//                    Constant.SaftyCountryIndex_Show = 13;
//                    break;
//                case Constant.SWITCH_SCI_REQUEST_PACKAGE1 /*24*/:
//                    Constant.Inverter_safty_country = "FRENCH 60Hz";
//                    Constant.SaftyCountryIndex = 24;
//                    Constant.SaftyCountryIndex_Show = 14;
//                    break;
//                case Constant.SWITCH_SCI_RESPONSE_PACKAGE1 /*25*/:
//                    Constant.Inverter_safty_country = "AUSTRALIA ERGON";
//                    Constant.SaftyCountryIndex = 25;
//                    Constant.SaftyCountryIndex_Show = 3;
//                    break;
//                case (byte) 26:
//                    Constant.Inverter_safty_country = "AUSTRALIA ENERGEX";
//                    Constant.SaftyCountryIndex = 26;
//                    Constant.SaftyCountryIndex_Show = flagsetworkmode;
//                    break;
//                case (byte) 27:
//                    Constant.Inverter_safty_country = "HOLLAND 16/20A";
//                    Constant.SaftyCountryIndex = 27;
//                    Constant.SaftyCountryIndex_Show = 20;
//                    break;
//                case (byte) 28:
//                    Constant.Inverter_safty_country = "KOREA";
//                    Constant.SaftyCountryIndex = 28;
//                    Constant.SaftyCountryIndex_Show = 24;
//                    break;
//                case Constant.BATTERY_MIN /*30*/:
//                    Constant.Inverter_safty_country = "AUSTRIA";
//                    Constant.SaftyCountryIndex = 30;
//                    Constant.SaftyCountryIndex_Show = 5;
//                    break;
//                case (byte) 31:
//                    Constant.Inverter_safty_country = "INDIA";
//                    Constant.SaftyCountryIndex = 31;
//                    Constant.SaftyCountryIndex_Show = 21;
//                    break;
//                case Constant.EDAY_INDEX /*32*/:
//                    Constant.Inverter_safty_country = "50Hz GRID DEFAULT";
//                    Constant.SaftyCountryIndex = 32;
//                    Constant.SaftyCountryIndex_Show = flagcollect;
//                    break;
//                case (byte) 34:
//                    Constant.Inverter_safty_country = "PHILLIPINES";
//                    Constant.SaftyCountryIndex = 34;
//                    Constant.SaftyCountryIndex_Show = 26;
//                    break;
//                case (byte) 35:
//                    Constant.Inverter_safty_country = "IRELAND";
//                    Constant.SaftyCountryIndex = 35;
//                    Constant.SaftyCountryIndex_Show = 22;
//                    break;
//                default:
//                    Constant.Inverter_safty_country = "50Hz GRID DEFAULT";
//                    break;
//            }
//            return true;
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//            return false;
//        }
//    }

//    public static boolean setGuanji() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(49));
//            if (dcUtil.checkPackageType(confirmData) != 48) {
//                return false;
//            }
//            byte[] data = dcUtil.fetchData(confirmData);
//            if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                return true;
//            }
//            return false;
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//            return false;
//        }
//    }

//    public static boolean setStoreEnergyMode() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(47));
//            if (dcUtil.checkPackageType(confirmData) != 48) {
//                return false;
//            }
//            byte[] data = dcUtil.fetchData(confirmData);
//            if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                return true;
//            }
//            return false;
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//            return false;
//        }
//    }

//    public static boolean setShadowScan() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(60));
//            if (dcUtil.checkPackageType(confirmData) != 61) {
//                return false;
//            }
//            byte[] data = dcUtil.fetchData(confirmData);
//            if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                return true;
//            }
//            return false;
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//            return false;
//        }
//    }

//    public static boolean SetOffCharge() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(62));
//            if (dcUtil.checkPackageType(confirmData) != 63) {
//                return false;
//            }
//            byte[] data = dcUtil.fetchData(confirmData);
//            if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                return true;
//            }
//            return false;
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//            return false;
//        }
//    }

//    public static boolean setInventerDateTime() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(10));
//            if (dcUtil.checkPackageType(confirmData) != 11) {
//                return false;
//            }
//            byte[] data = dcUtil.fetchData(confirmData);
//            if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                return true;
//            }
//            return false;
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//            return false;
//        }
//    }

//    public static boolean setInventerSaftyCountry() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(99));
//            if (dcUtil.checkPackageType(confirmData) != 100) {
//                return false;
//            }
//            byte[] data = dcUtil.fetchData(confirmData);
//            if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                return true;
//            }
//            return false;
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//            return false;
//        }
//    }

//    public static boolean setBackflowPrevention() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(57));
//            if (dcUtil.checkPackageType(confirmData) != 58) {
//                return false;
//            }
//            byte[] data = dcUtil.fetchData(confirmData);
//            if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                return true;
//            }
//            return false;
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//            return false;
//        }
//    }

//    public static boolean setGridUpPowerLimit() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(66));
//            if (dcUtil.checkPackageType(confirmData) != 67) {
//                return false;
//            }
//            byte[] data = dcUtil.fetchData(confirmData);
//            if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                return true;
//            }
//            return false;
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//            return false;
//        }
//    }

//    public static boolean setInventerSwitchSCI() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(12));
//            if (dcUtil.checkPackageType(confirmData) == 13) {
//                byte[] data = dcUtil.fetchData(confirmData);
//                if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                    return true;
//                }
//            }
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//        }
//        return false;
//    }

//    public static boolean setInventerrecoverSCI() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(24));
//            if (dcUtil.checkPackageType(confirmData) == 25) {
//                byte[] data = dcUtil.fetchData(confirmData);
//                if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                    return true;
//                }
//            }
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//        }
//        return false;
//    }

//    public static boolean setLeadAcidCmd() {
//        try {
//            Constant.CmdFlag = true;
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(Constant.LeadAcid_REQUEST_PACKAGE));
//            if (dcUtil.checkPackageType(confirmData) == Constant.LeadAcid_RESPONSE_PACKAGE) {
//                byte[] data = dcUtil.fetchData(confirmData);
//                if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                    return true;
//                }
//            }
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//        }
//        return false;
//    }

//    public static boolean setLeadCharge_V_I_Cmd() {
//        try {
//            Constant.CmdFlag = true;
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(Constant.Lead_Charge_V_I_REQUEST_PACKAGE));
//            if (dcUtil.checkPackageType(confirmData) == Constant.Lead_Charge_V_I_RESPONSE_PACKAGE) {
//                byte[] data = dcUtil.fetchData(confirmData);
//                if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                    return true;
//                }
//            }
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//        }
//        return false;
//    }

//    public static boolean setLeadDischarge_V_I_Cmd() {
//        try {
//            Constant.CmdFlag = true;
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(Constant.Lead_Discharge_V_I_REQUEST_PACKAGE));
//            if (dcUtil.checkPackageType(confirmData) == Constant.Lead_Discharge_V_I_RESPONSE_PACKAGE) {
//                byte[] data = dcUtil.fetchData(confirmData);
//                if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                    return true;
//                }
//            }
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//        }
//        return false;
//    }

//    public static boolean setBMSCmd() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(18));
//            if (dcUtil.checkPackageType(confirmData) == 19) {
//                byte[] data = dcUtil.fetchData(confirmData);
//                if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                    return true;
//                }
//            }
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//        }
//        return false;
//    }

//    public static boolean setBatteryInformation() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(20));
//            if (dcUtil.checkPackageType(confirmData) == 21) {
//                byte[] data = dcUtil.fetchData(confirmData);
//                if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                    return true;
//                }
//            }
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//        }
//        return false;
//    }

//    public static boolean setBMSReplyPacket() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(22));
//            if (dcUtil.checkPackageType(confirmData) == 23) {
//                byte[] data = dcUtil.fetchData(confirmData);
//                if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                    return true;
//                }
//            }
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//        }
//        return false;
//    }

//    public static boolean setLimitPower() {
//        try {
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(16));
//            if (dcUtil.checkPackageType(confirmData) == 17) {
//                byte[] data = dcUtil.fetchData(confirmData);
//                if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                    return true;
//                }
//            }
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//        }
//        return false;
//    }

//    public static boolean setLimitPowerForChargeandDischarge() {
//        return setLimitPowerForCharge() && setLimitPowerForDischarge();
//    }

//    public static boolean setLimitPowerForCharge() {
//        try {
//            if (Constant.Time_begin_charge == null || Constant.Time_end_charge == null) {
//                Log.e(TAG, "--\u5145\u7535\u65f6\u95f4\u4e3aNULL");
//                return false;
//            }
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(53));
//            if (dcUtil.checkPackageType(confirmData) == 54) {
//                byte[] data = dcUtil.fetchData(confirmData);
//                if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                    return true;
//                }
//            }
//            return false;
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//        }
//    }

//    public static boolean setLimitPowerForDischarge() {
//        try {
//            if (Constant.Time_begin_discharge == null || Constant.Time_end_discharge == null) {
//                Log.e(TAG, "--\u653e\u7535\u65f6\u95f4\u4e3aNULL");
//                return false;
//            }
//            byte[] confirmData = dcUtil.sendForData(dcUtil.buildPackage(55));
//            if (dcUtil.checkPackageType(confirmData) == 56) {
//                byte[] data = dcUtil.fetchData(confirmData);
//                if (data != null && data.length == flagsettime && data[flagcollect] == (byte) 6) {
//                    return true;
//                }
//            }
//            return false;
//        } catch (Exception e) {
//            Log.e(TAG, e.toString());
//        }
//    }

//    public boolean GetLimitPowerRate() {
//        Boolean bTime1;
//        Boolean bTime2;
//        Boolean bTime3;
//        byte Time1_Hour_begin;
//        byte Time1_Minute_begin;
//        byte Time1_Hour_end;
//        byte Time1_Minute_end;
//        byte Time2_Hour_begin;
//        byte Time2_Minute_begin;
//        byte Time2_Hour_end;
//        byte Time2_Minute_end;
//        byte Time3_Hour_begin;
//        byte Time3_Minute_begin;
//        byte Time3_Hour_end;
//        byte Time3_Minute_end;
//        int iTime1PriceUnit;
//        int iTime2PriceUnit;
//        int iTime3PriceUnit;
//        String strTime1Begin;
//        String strTime1End;
//        String strTime1PriceUnit;
//        String[] strArray;
//        String strTime2Begin;
//        String strTime2End;
//        String strTime2PriceUnit;
//        String strTime3Begin;
//        String strTime3End;
//        String strTime3PriceUnit;
//        Calendar cal;
//        Byte hour;
//        Byte min;
//        if (Constant.REL_LOADPOWER > 4600.0d) {
//            bTime1 = Boolean.valueOf(false);
//            bTime2 = Boolean.valueOf(false);
//            bTime3 = Boolean.valueOf(false);
//            Time1_Hour_begin = (byte) 0;
//            Time1_Minute_begin = (byte) 0;
//            Time1_Hour_end = (byte) 0;
//            Time1_Minute_end = (byte) 0;
//            Time2_Hour_begin = (byte) 0;
//            Time2_Minute_begin = (byte) 0;
//            Time2_Hour_end = (byte) 0;
//            Time2_Minute_end = (byte) 0;
//            Time3_Hour_begin = (byte) 0;
//            Time3_Minute_begin = (byte) 0;
//            Time3_Hour_end = (byte) 0;
//            Time3_Minute_end = (byte) 0;
//            iTime1PriceUnit = flagcollect;
//            iTime2PriceUnit = flagcollect;
//            iTime3PriceUnit = flagcollect;
//            strTime1Begin = PropertyUtil.GetValue(context, "Time1_begin");
//            strTime1End = PropertyUtil.GetValue(context, "Time1_end");
//            strTime1PriceUnit = PropertyUtil.GetValue(context, "editText_time1_price");
//            if (!(strTime1Begin.isEmpty() || strTime1End.isEmpty() || strTime1PriceUnit.isEmpty())) {
//                iTime1PriceUnit = Integer.valueOf(strTime1PriceUnit).intValue();
//                if (iTime1PriceUnit != 0) {
//                    strArray = strTime1Begin.split(":");
//                    Time1_Hour_begin = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time1_Minute_begin = Integer.valueOf(strArray[flagsettime]).intValue();
//                    strArray = strTime1End.split(":");
//                    Time1_Hour_end = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time1_Minute_end = Integer.valueOf(strArray[flagsettime]).intValue();
//                    bTime1 = Boolean.valueOf(true);
//                }
//            }
//            strTime2Begin = PropertyUtil.GetValue(context, "Time2_begin");
//            strTime2End = PropertyUtil.GetValue(context, "Time2_end");
//            strTime2PriceUnit = PropertyUtil.GetValue(context, "editText_time2_price");
//            if (!(strTime2Begin.isEmpty() || strTime2End.isEmpty() || strTime2PriceUnit.isEmpty())) {
//                iTime2PriceUnit = Integer.valueOf(strTime2PriceUnit).intValue();
//                if (iTime2PriceUnit != 0) {
//                    strArray = strTime2Begin.split(":");
//                    Time2_Hour_begin = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time2_Minute_begin = Integer.valueOf(strArray[flagsettime]).intValue();
//                    strArray = strTime2End.split(":");
//                    Time2_Hour_end = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time2_Minute_end = Integer.valueOf(strArray[flagsettime]).intValue();
//                    bTime2 = Boolean.valueOf(true);
//                }
//            }
//            strTime3Begin = PropertyUtil.GetValue(context, "Time3_begin");
//            strTime3End = PropertyUtil.GetValue(context, "Time3_end");
//            strTime3PriceUnit = PropertyUtil.GetValue(context, "editText_time3_price");
//            if (!(strTime3Begin.isEmpty() || strTime3End.isEmpty() || strTime3PriceUnit.isEmpty())) {
//                iTime3PriceUnit = Integer.valueOf(strTime3PriceUnit).intValue();
//                if (iTime3PriceUnit != 0) {
//                    strArray = strTime3Begin.split(":");
//                    Time3_Hour_begin = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time3_Minute_begin = Integer.valueOf(strArray[flagsettime]).intValue();
//                    strArray = strTime3End.split(":");
//                    Time3_Hour_end = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time3_Minute_end = Integer.valueOf(strArray[flagsettime]).intValue();
//                    bTime3 = Boolean.valueOf(true);
//                }
//            }
//            cal = Calendar.getInstance();
//            hour = Byte.valueOf(Integer.valueOf(cal.get(11)).byteValue());
//            min = Byte.valueOf(Integer.valueOf(cal.get(12)).byteValue());
//            if ((hour.byteValue() > Time1_Hour_begin || (hour.byteValue() == Time1_Hour_begin && min.byteValue() >= Time1_Minute_begin)) && (hour.byteValue() < Time1_Hour_end || (hour.byteValue() == Time1_Hour_end && min.byteValue() <= Time1_Minute_end))) {
//                if (bTime1.booleanValue()) {
//                    if (bTime2.booleanValue() && bTime3.booleanValue()) {
//                        if (iTime1PriceUnit >= iTime2PriceUnit && iTime1PriceUnit >= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                            return true;
//                        } else if (iTime1PriceUnit >= iTime2PriceUnit && iTime1PriceUnit <= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime1PriceUnit / iTime3PriceUnit)) * 100));
//                            return true;
//                        } else if (iTime1PriceUnit >= iTime3PriceUnit && iTime1PriceUnit <= iTime2PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime1PriceUnit / iTime2PriceUnit)) * 100));
//                            return true;
//                        } else if (iTime1PriceUnit <= iTime3PriceUnit && iTime1PriceUnit <= iTime2PriceUnit && iTime2PriceUnit < iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime1PriceUnit / iTime3PriceUnit)) * 100));
//                            return true;
//                        } else if (iTime1PriceUnit <= iTime3PriceUnit && iTime1PriceUnit <= iTime2PriceUnit && iTime2PriceUnit > iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime1PriceUnit / iTime2PriceUnit)) * 100));
//                            return true;
//                        }
//                    } else if (!bTime2.booleanValue() || bTime3.booleanValue()) {
//                        if (!bTime3.booleanValue() || bTime2.booleanValue()) {
//                            if (!(bTime3.booleanValue() || bTime2.booleanValue())) {
//                                Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                                return true;
//                            }
//                        } else if (iTime1PriceUnit >= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                            return true;
//                        } else if (iTime1PriceUnit <= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime1PriceUnit / iTime3PriceUnit)) * 100));
//                            return true;
//                        }
//                    } else if (iTime1PriceUnit >= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                        return true;
//                    } else if (iTime1PriceUnit <= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime1PriceUnit / iTime2PriceUnit)) * 100));
//                        return true;
//                    }
//                }
//            } else if ((hour.byteValue() > Time2_Hour_begin || (hour.byteValue() == Time2_Hour_begin && min.byteValue() >= Time2_Minute_begin)) && (hour.byteValue() < Time2_Hour_end || (hour.byteValue() == Time2_Hour_end && min.byteValue() <= Time2_Minute_end))) {
//                if (bTime2.booleanValue()) {
//                    if (bTime1.booleanValue() && bTime3.booleanValue()) {
//                        if (iTime2PriceUnit >= iTime1PriceUnit && iTime2PriceUnit >= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                            return true;
//                        } else if (iTime2PriceUnit >= iTime1PriceUnit && iTime2PriceUnit <= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime2PriceUnit / iTime3PriceUnit)) * 100));
//                            return true;
//                        } else if (iTime2PriceUnit >= iTime3PriceUnit && iTime2PriceUnit <= iTime1PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime2PriceUnit / iTime1PriceUnit)) * 100));
//                            return true;
//                        } else if (iTime2PriceUnit <= iTime3PriceUnit && iTime2PriceUnit <= iTime2PriceUnit && iTime1PriceUnit < iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime2PriceUnit / iTime3PriceUnit)) * 100));
//                            return true;
//                        } else if (iTime2PriceUnit <= iTime3PriceUnit && iTime2PriceUnit <= iTime2PriceUnit && iTime1PriceUnit > iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime2PriceUnit / iTime1PriceUnit)) * 100));
//                            return true;
//                        }
//                    } else if (!bTime1.booleanValue() || bTime3.booleanValue()) {
//                        if (!bTime3.booleanValue() || bTime1.booleanValue()) {
//                            if (!(bTime3.booleanValue() || bTime1.booleanValue())) {
//                                Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                                return true;
//                            }
//                        } else if (iTime2PriceUnit >= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                            return true;
//                        } else if (iTime2PriceUnit <= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime2PriceUnit / iTime3PriceUnit)) * 100));
//                            return true;
//                        }
//                    } else if (iTime2PriceUnit >= iTime1PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                        return true;
//                    } else if (iTime2PriceUnit <= iTime1PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime2PriceUnit / iTime1PriceUnit)) * 100));
//                        return true;
//                    }
//                }
//            } else if ((hour.byteValue() > Time3_Hour_begin || (hour.byteValue() == Time3_Hour_begin && min.byteValue() >= Time3_Minute_begin)) && ((hour.byteValue() < Time3_Hour_end || (hour.byteValue() == Time3_Hour_end && min.byteValue() <= Time3_Minute_end)) && bTime3.booleanValue())) {
//                if (bTime1.booleanValue() && bTime2.booleanValue()) {
//                    if (iTime3PriceUnit >= iTime1PriceUnit && iTime3PriceUnit >= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                        return true;
//                    } else if (iTime3PriceUnit >= iTime1PriceUnit && iTime3PriceUnit <= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime3PriceUnit / iTime2PriceUnit)) * 100));
//                        return true;
//                    } else if (iTime3PriceUnit <= iTime1PriceUnit && iTime3PriceUnit >= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime3PriceUnit / iTime1PriceUnit)) * 100));
//                        return true;
//                    } else if (iTime3PriceUnit <= iTime1PriceUnit && iTime3PriceUnit <= iTime2PriceUnit && iTime1PriceUnit < iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime3PriceUnit / iTime2PriceUnit)) * 100));
//                        return true;
//                    } else if (iTime3PriceUnit <= iTime1PriceUnit && iTime3PriceUnit <= iTime2PriceUnit && iTime1PriceUnit > iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime3PriceUnit / iTime1PriceUnit)) * 100));
//                        return true;
//                    }
//                } else if (!bTime1.booleanValue() || bTime2.booleanValue()) {
//                    if (!bTime2.booleanValue() || bTime1.booleanValue()) {
//                        if (!(bTime2.booleanValue() || bTime1.booleanValue())) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                            return true;
//                        }
//                    } else if (iTime3PriceUnit >= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                        return true;
//                    } else if (iTime3PriceUnit <= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime3PriceUnit / iTime2PriceUnit)) * 100));
//                        return true;
//                    }
//                } else if (iTime3PriceUnit >= iTime1PriceUnit) {
//                    Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                    return true;
//                } else if (iTime3PriceUnit <= iTime1PriceUnit) {
//                    Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime3PriceUnit / iTime1PriceUnit)) * 100));
//                    return true;
//                }
//            }
//        } else if (Constant.REL_cbattery1 > 20.0d) {
//            int sell_power_limit;
//            int fangdian_power_limit;
//            bTime1 = Boolean.valueOf(false);
//            bTime2 = Boolean.valueOf(false);
//            bTime3 = Boolean.valueOf(false);
//            Time1_Hour_begin = (byte) 0;
//            Time1_Minute_begin = (byte) 0;
//            Time1_Hour_end = (byte) 0;
//            Time1_Minute_end = (byte) 0;
//            Time2_Hour_begin = (byte) 0;
//            Time2_Minute_begin = (byte) 0;
//            Time2_Hour_end = (byte) 0;
//            Time2_Minute_end = (byte) 0;
//            Time3_Hour_begin = (byte) 0;
//            Time3_Minute_begin = (byte) 0;
//            Time3_Hour_end = (byte) 0;
//            Time3_Minute_end = (byte) 0;
//            iTime1PriceUnit = flagcollect;
//            iTime2PriceUnit = flagcollect;
//            iTime3PriceUnit = flagcollect;
//            String strTemp = PropertyUtil.GetValue(context, "editText_sell_power_limit");
//            if (strTemp.isEmpty()) {
//                sell_power_limit = 100;
//            } else {
//                sell_power_limit = Integer.valueOf(strTemp).intValue();
//            }
//            strTemp = PropertyUtil.GetValue(context, "editText_fangdian_power_limit");
//            if (strTemp.isEmpty()) {
//                fangdian_power_limit = 100;
//            } else {
//                fangdian_power_limit = Integer.valueOf(strTemp).intValue();
//            }
//            strTime1Begin = PropertyUtil.GetValue(context, "Time1_begin_sell");
//            strTime1End = PropertyUtil.GetValue(context, "Time1_end_sell");
//            strTime1PriceUnit = PropertyUtil.GetValue(context, "editText_time1_price_sell");
//            if (!(strTime1Begin.isEmpty() || strTime1End.isEmpty() || strTime1PriceUnit.isEmpty())) {
//                iTime1PriceUnit = Integer.valueOf(strTime1PriceUnit).intValue();
//                if (iTime1PriceUnit != 0) {
//                    strArray = strTime1Begin.split(":");
//                    Time1_Hour_begin = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time1_Minute_begin = Integer.valueOf(strArray[flagsettime]).intValue();
//                    strArray = strTime1End.split(":");
//                    Time1_Hour_end = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time1_Minute_end = Integer.valueOf(strArray[flagsettime]).intValue();
//                    bTime1 = Boolean.valueOf(true);
//                }
//            }
//            strTime2Begin = PropertyUtil.GetValue(context, "Time2_begin_sell");
//            strTime2End = PropertyUtil.GetValue(context, "Time2_end_sell");
//            strTime2PriceUnit = PropertyUtil.GetValue(context, "editText_time2_price_sell");
//            if (!(strTime2Begin.isEmpty() || strTime2End.isEmpty() || strTime2PriceUnit.isEmpty())) {
//                iTime2PriceUnit = Integer.valueOf(strTime2PriceUnit).intValue();
//                if (iTime2PriceUnit != 0) {
//                    strArray = strTime2Begin.split(":");
//                    Time2_Hour_begin = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time2_Minute_begin = Integer.valueOf(strArray[flagsettime]).intValue();
//                    strArray = strTime2End.split(":");
//                    Time2_Hour_end = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time2_Minute_end = Integer.valueOf(strArray[flagsettime]).intValue();
//                    bTime2 = Boolean.valueOf(true);
//                }
//            }
//            strTime3Begin = PropertyUtil.GetValue(context, "Time3_begin_sell");
//            strTime3End = PropertyUtil.GetValue(context, "Time3_end_sell");
//            strTime3PriceUnit = PropertyUtil.GetValue(context, "editText_time3_price_sell");
//            if (!(strTime3Begin.isEmpty() || strTime3End.isEmpty() || strTime3PriceUnit.isEmpty())) {
//                iTime3PriceUnit = Integer.valueOf(strTime3PriceUnit).intValue();
//                if (iTime3PriceUnit != 0) {
//                    strArray = strTime3Begin.split(":");
//                    Time3_Hour_begin = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time3_Minute_begin = Integer.valueOf(strArray[flagsettime]).intValue();
//                    strArray = strTime3End.split(":");
//                    Time3_Hour_end = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time3_Minute_end = Integer.valueOf(strArray[flagsettime]).intValue();
//                    bTime3 = Boolean.valueOf(true);
//                }
//            }
//            cal = Calendar.getInstance();
//            hour = Byte.valueOf(Integer.valueOf(cal.get(11)).byteValue());
//            min = Byte.valueOf(Integer.valueOf(cal.get(12)).byteValue());
//            if ((hour.byteValue() > Time1_Hour_begin || (hour.byteValue() == Time1_Hour_begin && min.byteValue() >= Time1_Minute_begin)) && (hour.byteValue() < Time1_Hour_end || (hour.byteValue() == Time1_Hour_end && min.byteValue() <= Time1_Minute_end))) {
//                if (bTime1.booleanValue()) {
//                    if (bTime2.booleanValue() && bTime3.booleanValue()) {
//                        if (iTime1PriceUnit >= iTime2PriceUnit && iTime1PriceUnit >= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min(100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        } else if (iTime1PriceUnit >= iTime2PriceUnit && iTime1PriceUnit <= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime1PriceUnit / iTime3PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        } else if (iTime1PriceUnit >= iTime3PriceUnit && iTime1PriceUnit <= iTime2PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime1PriceUnit / iTime2PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        } else if (iTime1PriceUnit <= iTime3PriceUnit && iTime1PriceUnit <= iTime2PriceUnit && iTime2PriceUnit < iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime1PriceUnit / iTime3PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        } else if (iTime1PriceUnit <= iTime3PriceUnit && iTime1PriceUnit <= iTime2PriceUnit && iTime2PriceUnit > iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime1PriceUnit / iTime2PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        }
//                    } else if (!bTime2.booleanValue() || bTime3.booleanValue()) {
//                        if (!bTime3.booleanValue() || bTime2.booleanValue()) {
//                            if (!(bTime3.booleanValue() || bTime2.booleanValue())) {
//                                Constant.LimitPowerRate = Byte.valueOf((byte) min(100, sell_power_limit, fangdian_power_limit));
//                                return true;
//                            }
//                        } else if (iTime1PriceUnit >= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min(100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        } else if (iTime1PriceUnit <= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime1PriceUnit / iTime3PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        }
//                    } else if (iTime1PriceUnit >= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) min(100, sell_power_limit, fangdian_power_limit));
//                        return true;
//                    } else if (iTime1PriceUnit <= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime1PriceUnit / iTime2PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                        return true;
//                    }
//                }
//            } else if ((hour.byteValue() > Time2_Hour_begin || (hour.byteValue() == Time2_Hour_begin && min.byteValue() >= Time2_Minute_begin)) && (hour.byteValue() < Time2_Hour_end || (hour.byteValue() == Time2_Hour_end && min.byteValue() <= Time2_Minute_end))) {
//                if (bTime2.booleanValue()) {
//                    if (bTime1.booleanValue() && bTime3.booleanValue()) {
//                        if (iTime2PriceUnit >= iTime1PriceUnit && iTime2PriceUnit >= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min(100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        } else if (iTime2PriceUnit >= iTime1PriceUnit && iTime2PriceUnit <= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime2PriceUnit / iTime3PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        } else if (iTime2PriceUnit >= iTime3PriceUnit && iTime2PriceUnit <= iTime1PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime2PriceUnit / iTime1PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        } else if (iTime2PriceUnit <= iTime3PriceUnit && iTime2PriceUnit <= iTime2PriceUnit && iTime1PriceUnit < iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime2PriceUnit / iTime3PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        } else if (iTime2PriceUnit <= iTime3PriceUnit && iTime2PriceUnit <= iTime2PriceUnit && iTime1PriceUnit > iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime2PriceUnit / iTime1PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        }
//                    } else if (!bTime1.booleanValue() || bTime3.booleanValue()) {
//                        if (!bTime3.booleanValue() || bTime1.booleanValue()) {
//                            if (!(bTime3.booleanValue() || bTime1.booleanValue())) {
//                                Constant.LimitPowerRate = Byte.valueOf((byte) min(100, sell_power_limit, fangdian_power_limit));
//                                return true;
//                            }
//                        } else if (iTime2PriceUnit >= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min(100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        } else if (iTime2PriceUnit <= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime2PriceUnit / iTime3PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        }
//                    } else if (iTime2PriceUnit >= iTime1PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) min(100, sell_power_limit, fangdian_power_limit));
//                        return true;
//                    } else if (iTime2PriceUnit <= iTime1PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime2PriceUnit / iTime1PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                        return true;
//                    }
//                }
//            } else if ((hour.byteValue() > Time3_Hour_begin || (hour.byteValue() == Time3_Hour_begin && min.byteValue() >= Time3_Minute_begin)) && ((hour.byteValue() < Time3_Hour_end || (hour.byteValue() == Time3_Hour_end && min.byteValue() <= Time3_Minute_end)) && bTime3.booleanValue())) {
//                if (bTime1.booleanValue() && bTime2.booleanValue()) {
//                    if (iTime3PriceUnit >= iTime1PriceUnit && iTime3PriceUnit >= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) min(100, sell_power_limit, fangdian_power_limit));
//                        return true;
//                    } else if (iTime3PriceUnit >= iTime1PriceUnit && iTime3PriceUnit <= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime3PriceUnit / iTime2PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                        return true;
//                    } else if (iTime3PriceUnit <= iTime1PriceUnit && iTime3PriceUnit >= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime3PriceUnit / iTime1PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                        return true;
//                    } else if (iTime3PriceUnit <= iTime1PriceUnit && iTime3PriceUnit <= iTime2PriceUnit && iTime1PriceUnit < iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime3PriceUnit / iTime2PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                        return true;
//                    } else if (iTime3PriceUnit <= iTime1PriceUnit && iTime3PriceUnit <= iTime2PriceUnit && iTime1PriceUnit > iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime3PriceUnit / iTime1PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                        return true;
//                    }
//                } else if (!bTime1.booleanValue() || bTime2.booleanValue()) {
//                    if (!bTime2.booleanValue() || bTime1.booleanValue()) {
//                        if (!(bTime2.booleanValue() || bTime1.booleanValue())) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) min(100, sell_power_limit, fangdian_power_limit));
//                            return true;
//                        }
//                    } else if (iTime3PriceUnit >= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) min(100, sell_power_limit, fangdian_power_limit));
//                        return true;
//                    } else if (iTime3PriceUnit <= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime3PriceUnit / iTime2PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                        return true;
//                    }
//                } else if (iTime3PriceUnit >= iTime1PriceUnit) {
//                    Constant.LimitPowerRate = Byte.valueOf((byte) min(100, sell_power_limit, fangdian_power_limit));
//                    return true;
//                } else if (iTime3PriceUnit <= iTime1PriceUnit) {
//                    Constant.LimitPowerRate = Byte.valueOf((byte) min((iTime3PriceUnit / iTime1PriceUnit) * 100, sell_power_limit, fangdian_power_limit));
//                    return true;
//                }
//            }
//        } else {
//            bTime1 = Boolean.valueOf(false);
//            bTime2 = Boolean.valueOf(false);
//            bTime3 = Boolean.valueOf(false);
//            Time1_Hour_begin = (byte) 0;
//            Time1_Minute_begin = (byte) 0;
//            Time1_Hour_end = (byte) 0;
//            Time1_Minute_end = (byte) 0;
//            Time2_Hour_begin = (byte) 0;
//            Time2_Minute_begin = (byte) 0;
//            Time2_Hour_end = (byte) 0;
//            Time2_Minute_end = (byte) 0;
//            Time3_Hour_begin = (byte) 0;
//            Time3_Minute_begin = (byte) 0;
//            Time3_Hour_end = (byte) 0;
//            Time3_Minute_end = (byte) 0;
//            iTime1PriceUnit = flagcollect;
//            iTime2PriceUnit = flagcollect;
//            iTime3PriceUnit = flagcollect;
//            strTime1Begin = PropertyUtil.GetValue(context, "Time1_begin");
//            strTime1End = PropertyUtil.GetValue(context, "Time1_end");
//            strTime1PriceUnit = PropertyUtil.GetValue(context, "editText_time1_price");
//            if (!(strTime1Begin.isEmpty() || strTime1End.isEmpty() || strTime1PriceUnit.isEmpty())) {
//                iTime1PriceUnit = Integer.valueOf(strTime1PriceUnit).intValue();
//                if (iTime1PriceUnit != 0) {
//                    strArray = strTime1Begin.split(":");
//                    Time1_Hour_begin = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time1_Minute_begin = Integer.valueOf(strArray[flagsettime]).intValue();
//                    strArray = strTime1End.split(":");
//                    Time1_Hour_end = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time1_Minute_end = Integer.valueOf(strArray[flagsettime]).intValue();
//                    bTime1 = Boolean.valueOf(true);
//                }
//            }
//            strTime2Begin = PropertyUtil.GetValue(context, "Time2_begin");
//            strTime2End = PropertyUtil.GetValue(context, "Time2_end");
//            strTime2PriceUnit = PropertyUtil.GetValue(context, "editText_time2_price");
//            if (!(strTime2Begin.isEmpty() || strTime2End.isEmpty() || strTime2PriceUnit.isEmpty())) {
//                iTime2PriceUnit = Integer.valueOf(strTime2PriceUnit).intValue();
//                if (iTime2PriceUnit != 0) {
//                    strArray = strTime2Begin.split(":");
//                    Time2_Hour_begin = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time2_Minute_begin = Integer.valueOf(strArray[flagsettime]).intValue();
//                    strArray = strTime2End.split(":");
//                    Time2_Hour_end = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time2_Minute_end = Integer.valueOf(strArray[flagsettime]).intValue();
//                    bTime2 = Boolean.valueOf(true);
//                }
//            }
//            strTime3Begin = PropertyUtil.GetValue(context, "Time3_begin");
//            strTime3End = PropertyUtil.GetValue(context, "Time3_end");
//            strTime3PriceUnit = PropertyUtil.GetValue(context, "editText_time3_price");
//            if (!(strTime3Begin.isEmpty() || strTime3End.isEmpty() || strTime3PriceUnit.isEmpty())) {
//                iTime3PriceUnit = Integer.valueOf(strTime3PriceUnit).intValue();
//                if (iTime3PriceUnit != 0) {
//                    strArray = strTime3Begin.split(":");
//                    Time3_Hour_begin = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time3_Minute_begin = Integer.valueOf(strArray[flagsettime]).intValue();
//                    strArray = strTime3End.split(":");
//                    Time3_Hour_end = Integer.valueOf(strArray[flagcollect]).intValue();
//                    Time3_Minute_end = Integer.valueOf(strArray[flagsettime]).intValue();
//                    bTime3 = Boolean.valueOf(true);
//                }
//            }
//            cal = Calendar.getInstance();
//            hour = Byte.valueOf(Integer.valueOf(cal.get(11)).byteValue());
//            min = Byte.valueOf(Integer.valueOf(cal.get(12)).byteValue());
//            if ((hour.byteValue() > Time1_Hour_begin || (hour.byteValue() == Time1_Hour_begin && min.byteValue() >= Time1_Minute_begin)) && (hour.byteValue() < Time1_Hour_end || (hour.byteValue() == Time1_Hour_end && min.byteValue() <= Time1_Minute_end))) {
//                if (bTime1.booleanValue()) {
//                    if (bTime2.booleanValue() && bTime3.booleanValue()) {
//                        if (iTime1PriceUnit >= iTime2PriceUnit && iTime1PriceUnit >= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                            return true;
//                        } else if (iTime1PriceUnit >= iTime2PriceUnit && iTime1PriceUnit <= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime1PriceUnit / iTime3PriceUnit)) * 100));
//                            return true;
//                        } else if (iTime1PriceUnit >= iTime3PriceUnit && iTime1PriceUnit <= iTime2PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime1PriceUnit / iTime2PriceUnit)) * 100));
//                            return true;
//                        } else if (iTime1PriceUnit <= iTime3PriceUnit && iTime1PriceUnit <= iTime2PriceUnit && iTime2PriceUnit < iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime1PriceUnit / iTime3PriceUnit)) * 100));
//                            return true;
//                        } else if (iTime1PriceUnit <= iTime3PriceUnit && iTime1PriceUnit <= iTime2PriceUnit && iTime2PriceUnit > iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime1PriceUnit / iTime2PriceUnit)) * 100));
//                            return true;
//                        }
//                    } else if (!bTime2.booleanValue() || bTime3.booleanValue()) {
//                        if (!bTime3.booleanValue() || bTime2.booleanValue()) {
//                            if (!(bTime3.booleanValue() || bTime2.booleanValue())) {
//                                Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                                return true;
//                            }
//                        } else if (iTime1PriceUnit >= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                            return true;
//                        } else if (iTime1PriceUnit <= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime1PriceUnit / iTime3PriceUnit)) * 100));
//                            return true;
//                        }
//                    } else if (iTime1PriceUnit >= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                        return true;
//                    } else if (iTime1PriceUnit <= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime1PriceUnit / iTime2PriceUnit)) * 100));
//                        return true;
//                    }
//                }
//            } else if ((hour.byteValue() > Time2_Hour_begin || (hour.byteValue() == Time2_Hour_begin && min.byteValue() >= Time2_Minute_begin)) && (hour.byteValue() < Time2_Hour_end || (hour.byteValue() == Time2_Hour_end && min.byteValue() <= Time2_Minute_end))) {
//                if (bTime2.booleanValue()) {
//                    if (bTime1.booleanValue() && bTime3.booleanValue()) {
//                        if (iTime2PriceUnit >= iTime1PriceUnit && iTime2PriceUnit >= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                            return true;
//                        } else if (iTime2PriceUnit >= iTime1PriceUnit && iTime2PriceUnit <= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime2PriceUnit / iTime3PriceUnit)) * 100));
//                            return true;
//                        } else if (iTime2PriceUnit >= iTime3PriceUnit && iTime2PriceUnit <= iTime1PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime2PriceUnit / iTime1PriceUnit)) * 100));
//                            return true;
//                        } else if (iTime2PriceUnit <= iTime3PriceUnit && iTime2PriceUnit <= iTime2PriceUnit && iTime1PriceUnit < iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime2PriceUnit / iTime3PriceUnit)) * 100));
//                            return true;
//                        } else if (iTime2PriceUnit <= iTime3PriceUnit && iTime2PriceUnit <= iTime2PriceUnit && iTime1PriceUnit > iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime2PriceUnit / iTime1PriceUnit)) * 100));
//                            return true;
//                        }
//                    } else if (!bTime1.booleanValue() || bTime3.booleanValue()) {
//                        if (!bTime3.booleanValue() || bTime1.booleanValue()) {
//                            if (!(bTime3.booleanValue() || bTime1.booleanValue())) {
//                                Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                                return true;
//                            }
//                        } else if (iTime2PriceUnit >= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                            return true;
//                        } else if (iTime2PriceUnit <= iTime3PriceUnit) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime2PriceUnit / iTime3PriceUnit)) * 100));
//                            return true;
//                        }
//                    } else if (iTime2PriceUnit >= iTime1PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                        return true;
//                    } else if (iTime2PriceUnit <= iTime1PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime2PriceUnit / iTime1PriceUnit)) * 100));
//                        return true;
//                    }
//                }
//            } else if ((hour.byteValue() > Time3_Hour_begin || (hour.byteValue() == Time3_Hour_begin && min.byteValue() >= Time3_Minute_begin)) && ((hour.byteValue() < Time3_Hour_end || (hour.byteValue() == Time3_Hour_end && min.byteValue() <= Time3_Minute_end)) && bTime3.booleanValue())) {
//                if (bTime1.booleanValue() && bTime2.booleanValue()) {
//                    if (iTime3PriceUnit >= iTime1PriceUnit && iTime3PriceUnit >= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                        return true;
//                    } else if (iTime3PriceUnit >= iTime1PriceUnit && iTime3PriceUnit <= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime3PriceUnit / iTime2PriceUnit)) * 100));
//                        return true;
//                    } else if (iTime3PriceUnit <= iTime1PriceUnit && iTime3PriceUnit >= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime3PriceUnit / iTime1PriceUnit)) * 100));
//                        return true;
//                    } else if (iTime3PriceUnit <= iTime1PriceUnit && iTime3PriceUnit <= iTime2PriceUnit && iTime1PriceUnit < iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime3PriceUnit / iTime2PriceUnit)) * 100));
//                        return true;
//                    } else if (iTime3PriceUnit <= iTime1PriceUnit && iTime3PriceUnit <= iTime2PriceUnit && iTime1PriceUnit > iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime3PriceUnit / iTime1PriceUnit)) * 100));
//                        return true;
//                    }
//                } else if (!bTime1.booleanValue() || bTime2.booleanValue()) {
//                    if (!bTime2.booleanValue() || bTime1.booleanValue()) {
//                        if (!(bTime2.booleanValue() || bTime1.booleanValue())) {
//                            Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                            return true;
//                        }
//                    } else if (iTime3PriceUnit >= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                        return true;
//                    } else if (iTime3PriceUnit <= iTime2PriceUnit) {
//                        Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime3PriceUnit / iTime2PriceUnit)) * 100));
//                        return true;
//                    }
//                } else if (iTime3PriceUnit >= iTime1PriceUnit) {
//                    Constant.LimitPowerRate = Byte.valueOf((byte) 0);
//                    return true;
//                } else if (iTime3PriceUnit <= iTime1PriceUnit) {
//                    Constant.LimitPowerRate = Byte.valueOf((byte) ((1 - (iTime3PriceUnit / iTime1PriceUnit)) * 100));
//                    return true;
//                }
//            }
//        }
//        return false;
//    }

//    int min(int i1, int i2, int i3) {
//        if (i1 <= i2 && i1 <= i3) {
//            return i1;
//        }
//        if (i2 <= i1 && i2 <= i3) {
//            return i2;
//        }
//        if (i3 > i1 || i3 > i2) {
//            return flagcollect;
//        }
//        return i3;
//    }

//    public static WifiConfiguration IsExsits(String SSID) {
//        for (WifiConfiguration existingConfig : wifiManager.getConfiguredNetworks()) {
//            if (existingConfig.SSID.equals("\"" + SSID + "\"")) {
//                return existingConfig;
//            }
//        }
//        return null;
//    }

//    public static boolean setBatteryCmd() throws UnsupportedEncodingException {
//        synchronized (sendlock) {
//            setInventerSwitchSCI();
//            if (setLeadAcidCmd() || setLeadAcidCmd() || setLeadAcidCmd()) {
//                setInventerrecoverSCI();
//                setInventerrecoverSCI();
//                return true;
//            }
//            setInventerrecoverSCI();
//            setInventerrecoverSCI();
//            return false;
//        }
//    }

//    public static boolean setBatteryChargeVICmd() {
//        setInventerSwitchSCI();
//        if (setLeadCharge_V_I_Cmd() || setLeadCharge_V_I_Cmd() || setLeadCharge_V_I_Cmd()) {
//            setInventerrecoverSCI();
//            setInventerrecoverSCI();
//            return true;
//        }
//        setInventerrecoverSCI();
//        setInventerrecoverSCI();
//        return false;
//    }

//    public static boolean setBatteryDischargeVICmd() {
//        setInventerSwitchSCI();
//        if (setLeadDischarge_V_I_Cmd() || setLeadDischarge_V_I_Cmd() || setLeadDischarge_V_I_Cmd()) {
//            setInventerrecoverSCI();
//            setInventerrecoverSCI();
//            return true;
//        }
//        setInventerrecoverSCI();
//        setInventerrecoverSCI();
//        return false;
//    }
//}