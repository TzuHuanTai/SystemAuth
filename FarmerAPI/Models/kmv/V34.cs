using System;
using System.Collections.Generic;

// V34 客戶 / 供應商資料檔
// V34_K=V3401+V3404
namespace FarmerAPI.Models
{
    public partial class V34
    {
        public string V3401 { get; set; }  //客戶 / 供應商代號
        public string V3402 { get; set; }  //客戶 / 供應商名稱
        public string V3403 { get; set; }  //客戶 / 供應商簡稱
        public decimal? V3404 { get; set; }  //1.客戶 2.供應商
        public decimal? V3405 { get; set; }  //客戶(供應商)區分 1.客戶(供應商) 2.其他
        public string V3406 { get; set; }   //負責人
        public string V3407 { get; set; }   //連絡人
        public string V3408 { get; set; }   //統一編號
        public string V3409 { get; set; }   //地區別代號
        public decimal? V3410 { get; set; }  //信用額度
        public string V3411 { get; set; }   //業務員
        public string V3412 { get; set; }   //電話
        public string V3413 { get; set; }   //傳真
        public string V3414 { get; set; }   //B.B.CALL
        public string V3415 { get; set; }   //大哥大
        public string V3416 { get; set; }  //國別代號
        public string V3417 { get; set; }  //幣別代號
        public decimal? V3418 { get; set; }  //發票否(0.否 1.是)
        public string V3419 { get; set; }  //客戶 / 供應商等級
        public string V3420 { get; set; }  //公司地址 / 英文地址1
        public string V3421 { get; set; }  //發票地址 / 英文地址2
        public string V3422 { get; set; }  //工廠地址 / 送貨地址
        public string V3423 { get; set; }  //備註
        public string V3424 { get; set; }  //客戶 / 供應商類別
        public string V3425 { get; set; }  //電話(1)
        public string V3426 { get; set; }  //電話(2)
        public decimal? V3427 { get; set; }  //付款方式	1.月結 2.貨到
        public decimal? V3428 { get; set; }  //帳款天數 / 票據到期
        public decimal? V3429 { get; set; }  //通路
        public string V3430 { get; set; }  //課稅別 1.應稅 2.零稅 3.免稅
        public string V3431 { get; set; }  //發票種類 1.三聯電子 2.三聯收銀機 3.2聯 4.免開發票
        public decimal? V3432 { get; set; }  //結帳日
        public string V3433 { get; set; }  //匯款銀行
        public string V3434 { get; set; }  //匯款帳號
        public decimal? V3435 { get; set; }  //經度
        public decimal? V3436 { get; set; }  //緯度
        public string V3496 { get; set; }  //建立者
        public DateTime? V3497 { get; set; } //建立日期
        public string V3498 { get; set; }  //異動者
        public DateTime? V3499 { get; set; }  //異動日期
    }
}
