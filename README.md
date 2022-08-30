# BasicORM
Query Object Design Pattern ile geliştirilmiş basit bir orm aracıdır.
DALContext Sınıfı 
Sql İşlemleri yapmak için oluşturulan bir sınıf
GetOpenConnectionAsync Methodu
Sql Connectiona ulaşmak oluşturulan bir method sqlconnection kapalı ise yenisi oluşturup döndürür

CloseConnectionAsync Methodu
SqlConnection'ı kapatmak için kullanılan methodumuz
ExecuteQueryAsync methodu
Gelen query parametresi execute edip veritabanından dönen sonucu döner
-----------------------
Criteria<TModel> Sınıfı 
Sql Select sorgumuzda where clause ekler karşılaştırma işlemleri yapmamızı sağlar  
 
Query<T> Sınıfı
Query sınıfı _criterias listesine eklenen kriterlere göre bir sql sorgu komutu oluşturur
ve bu sonucu döndürür
