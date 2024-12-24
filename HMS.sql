--
-- PostgreSQL database dump
--

-- Dumped from database version 17.2
-- Dumped by pg_dump version 17.2

-- Started on 2024-12-24 20:36:13

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 267 (class 1255 OID 17333)
-- Name: calisan_arama(integer, text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.calisan_arama(arnan_id integer DEFAULT NULL::integer, arnan_ad text DEFAULT NULL::text) RETURNS TABLE(id integer, "adı" text, "Doğum Tarihi" date, cinsiyet character, telefon character, adres text, e_posta character varying, "İşe alınma tarihi" date, "Ünvan" character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT calisan.kisi_id AS "ID",
           CONCAT(kisi.ad,' ', kisi.soyad) AS "Adı",
           kisi.dogum_tarihi AS "Doğum Tarihi",
           kisi.cinsiyet,
           iletisim_bilgileri.telefon,
           iletisim_bilgileri.adres,
           iletisim_bilgileri.e_posta,
           calisan.ise_alinma_tarihi AS "İşe alınma tarihi",
           calisan.unvan AS "Ünvan"
    FROM calisan NATURAL JOIN kisi
    INNER JOIN iletisim_bilgileri ON kisi.iletisim_id = iletisim_bilgileri.id
    WHERE (arnan_id IS NULL OR calisan.kisi_id = arnan_id)
      AND (arnan_ad IS NULL OR CONCAT(kisi.ad, ' ', kisi.soyad) ILIKE '%' || arnan_ad || '%');
END;
$$;


ALTER FUNCTION public.calisan_arama(arnan_id integer, arnan_ad text) OWNER TO postgres;

--
-- TOC entry 256 (class 1255 OID 17312)
-- Name: calisan_ekle(character varying, character varying, date, character, date, character varying, character, text, character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.calisan_ekle(IN yad character varying, IN ysoyad character varying, IN ydogum_tarihi date, IN ycinsiyet character, IN yise_alma_tarihi date, IN yunvan character varying, IN ytelefon character, IN yadres text, IN ye_posta character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE
    iltsm_id INT;
    ykisi_id INT;
BEGIN
    INSERT INTO iletisim_bilgileri (telefon, adres, e_posta)
    VALUES (ytelefon, yadres, ye_posta) RETURNING id INTO iltsm_id;

    INSERT INTO kisi (ad, soyad, dogum_tarihi, cinsiyet, iletisim_id)
    VALUES (yad, ysoyad, ydogum_tarihi, ycinsiyet, iltsm_id)
    RETURNING kisi_id INTO ykisi_id;

    INSERT INTO calisan (kisi_id, ise_alinma_tarihi, unvan)
    VALUES (ykisi_id, yise_alma_tarihi, yunvan);
END;
$$;


ALTER PROCEDURE public.calisan_ekle(IN yad character varying, IN ysoyad character varying, IN ydogum_tarihi date, IN ycinsiyet character, IN yise_alma_tarihi date, IN yunvan character varying, IN ytelefon character, IN yadres text, IN ye_posta character varying) OWNER TO postgres;

--
-- TOC entry 261 (class 1255 OID 17338)
-- Name: calisan_guncelleme(integer, character varying, character varying, date, character, date, character varying, character, text, character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.calisan_guncelleme(IN y_kisi_id integer, IN y_ad character varying, IN y_soyad character varying, IN y_dogum_tarihi date, IN y_cinsiyet character, IN y_ise_alinma_tarihi date, IN y_unvan character varying, IN y_telefon character, IN y_adres text, IN y_e_posta character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    IF EXISTS (SELECT 1 FROM calisan WHERE kisi_id = y_kisi_id) THEN

        UPDATE iletisim_bilgileri
        SET telefon = y_telefon, adres = y_adres, e_posta = y_e_posta
        WHERE id = (SELECT iletisim_id FROM kisi WHERE kisi_id = y_kisi_id);

        UPDATE kisi
        SET ad = y_ad, soyad = y_soyad, dogum_tarihi = y_dogum_tarihi, cinsiyet = y_cinsiyet
        WHERE kisi_id = y_kisi_id;

        UPDATE calisan
        SET ise_alinma_tarihi = y_ise_alinma_tarihi, unvan = y_unvan
        WHERE kisi_id = y_kisi_id;

    ELSE
        RAISE NOTICE 'Bu çalısan ID % mevcut değildir.', y_kisi_id;
    END IF;
END;
$$;


ALTER PROCEDURE public.calisan_guncelleme(IN y_kisi_id integer, IN y_ad character varying, IN y_soyad character varying, IN y_dogum_tarihi date, IN y_cinsiyet character, IN y_ise_alinma_tarihi date, IN y_unvan character varying, IN y_telefon character, IN y_adres text, IN y_e_posta character varying) OWNER TO postgres;

--
-- TOC entry 266 (class 1255 OID 17336)
-- Name: calisan_silme(integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.calisan_silme(IN calisan_id integer)
    LANGUAGE plpgsql
    AS $$
DECLARE
    iltsm_id INT;
BEGIN
    IF EXISTS (SELECT 1 FROM calisan WHERE kisi_id = calisan_id) THEN

        SELECT kisi.iletisim_id INTO iltsm_id
        FROM kisi
        WHERE kisi_id = calisan_id;

        IF EXISTS (SELECT 1 FROM doktor WHERE kisi_id = calisan_id) THEN
            DELETE FROM doktor WHERE kisi_id = calisan_id;
        END IF;

        IF EXISTS (SELECT 1 FROM hemsire WHERE kisi_id = calisan_id) THEN
            DELETE FROM hemsire WHERE kisi_id = calisan_id;
        END IF;

        DELETE FROM calisan WHERE kisi_id = calisan_id;

        DELETE FROM kisi WHERE kisi_id = calisan_id;

        IF NOT EXISTS (SELECT 1 FROM kisi WHERE iletisim_id = iltsm_id) THEN
            DELETE FROM iletisim_bilgileri WHERE id = iltsm_id;
        END IF;

    ELSE
        RAISE NOTICE 'Bu calişan ID % mevcut değildir.', calisan_id;
    END IF;
END;
$$;


ALTER PROCEDURE public.calisan_silme(IN calisan_id integer) OWNER TO postgres;

--
-- TOC entry 259 (class 1255 OID 17322)
-- Name: calisanlar_yazdir(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.calisanlar_yazdir() RETURNS TABLE(id integer, "adı" text, "Doğum Tarihi" date, cinsiyet character, telefon character, adres text, e_posta character varying, "İşe alınma tarihi" date, "Ünvan" character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT calisan.kisi_id AS "ID",
           CONCAT(kisi.ad,' ', kisi.soyad) AS "Adı",
           kisi.dogum_tarihi AS "Doğum Tarihi",
           kisi.cinsiyet,
           iletisim_bilgileri.telefon,
           iletisim_bilgileri.adres,
           iletisim_bilgileri.e_posta,
           calisan.ise_alinma_tarihi AS "İşe alınma tarihi",
           calisan.unvan AS "Ünvan"
    FROM calisan NATURAL JOIN kisi
    INNER JOIN iletisim_bilgileri ON kisi.iletisim_id = iletisim_bilgileri.id;
END;
$$;


ALTER FUNCTION public.calisanlar_yazdir() OWNER TO postgres;

--
-- TOC entry 264 (class 1255 OID 17332)
-- Name: doktor_arama(integer, text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.doktor_arama(arnan_id integer DEFAULT NULL::integer, arnan_ad text DEFAULT NULL::text) RETURNS TABLE(id integer, "adı" text, "Doğum Tarihi" date, cinsiyet character, telefon character, adres text, e_posta character varying, "İşe alınma tarihi" date, "Ünvan" character varying, uzmanlik character varying, "Bölüm" character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT doktor.kisi_id AS "ID",
           CONCAT(kisi.ad,' ', kisi.soyad) AS "Adı",
           kisi.dogum_tarihi AS "Doğum Tarihi",
           kisi.cinsiyet,
           iletisim_bilgileri.telefon,
           iletisim_bilgileri.adres,
           iletisim_bilgileri.e_posta,
           calisan.ise_alinma_tarihi AS "İşe alınma tarihi",
           calisan.unvan AS "Ünvan",
           doktor.uzmanlik,
           bolum.ad AS "Bölüm"
    FROM doktor
    NATURAL JOIN calisan NATURAL JOIN kisi
    INNER JOIN iletisim_bilgileri ON kisi.iletisim_id = iletisim_bilgileri.id
    INNER JOIN bolum ON doktor.bolum_id = bolum.id
    WHERE (arnan_id IS NULL OR doktor.kisi_id = arnan_id)
      AND (arnan_ad IS NULL OR CONCAT(kisi.ad, ' ', kisi.soyad) ILIKE '%' || arnan_ad || '%');
END;
$$;


ALTER FUNCTION public.doktor_arama(arnan_id integer, arnan_ad text) OWNER TO postgres;

--
-- TOC entry 257 (class 1255 OID 17313)
-- Name: doktor_ekle(character varying, character varying, date, character, date, character varying, character varying, integer, character, text, character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.doktor_ekle(IN yad character varying, IN ysoyad character varying, IN ydogum_tarihi date, IN ycinsiyet character, IN yise_alma_tarihi date, IN yunvan character varying, IN yuzmanlik character varying, IN ybolum_id integer, IN ytelefon character, IN yadres text, IN ye_posta character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE
    iltsm_id INT;
    ykisi_id INT;
BEGIN
    INSERT INTO iletisim_bilgileri (telefon, adres, e_posta)
    VALUES (ytelefon, yadres, ye_posta) RETURNING id INTO iltsm_id;

    INSERT INTO kisi (ad, soyad, dogum_tarihi, cinsiyet, iletisim_id)
    VALUES (yad, ysoyad, ydogum_tarihi, ycinsiyet, iltsm_id)
    RETURNING kisi_id INTO ykisi_id;

    INSERT INTO calisan (kisi_id, ise_alinma_tarihi, unvan)
    VALUES (ykisi_id, yise_alma_tarihi, yunvan);

    INSERT INTO doktor (kisi_id, uzmanlik, bolum_id)
    VALUES (ykisi_id, yuzmanlik, ybolum_id);
END;
$$;


ALTER PROCEDURE public.doktor_ekle(IN yad character varying, IN ysoyad character varying, IN ydogum_tarihi date, IN ycinsiyet character, IN yise_alma_tarihi date, IN yunvan character varying, IN yuzmanlik character varying, IN ybolum_id integer, IN ytelefon character, IN yadres text, IN ye_posta character varying) OWNER TO postgres;

--
-- TOC entry 271 (class 1255 OID 17348)
-- Name: doktor_guncelleme(integer, character varying, character varying, date, character, date, character varying, character varying, integer, character, text, character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.doktor_guncelleme(IN y_kisi_id integer, IN y_ad character varying, IN y_soyad character varying, IN y_dogum_tarihi date, IN y_cinsiyet character, IN y_ise_alinma_tarihi date, IN y_unvan character varying, IN y_uzmanlik character varying, IN y_bolum_id integer, IN y_telefon character, IN y_adres text, IN y_e_posta character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    IF EXISTS (SELECT 1 FROM doktor WHERE kisi_id = y_kisi_id) THEN

    UPDATE iletisim_bilgileri
    SET telefon = y_telefon, adres = y_adres, e_posta = y_e_posta
    WHERE id = (SELECT iletisim_id FROM kisi WHERE kisi_id = y_kisi_id);

    UPDATE kisi
    SET ad = y_ad, soyad = y_soyad, dogum_tarihi = y_dogum_tarihi, cinsiyet = y_cinsiyet
    WHERE kisi_id = y_kisi_id;

    UPDATE calisan
    SET ise_alinma_tarihi = y_ise_alinma_tarihi, unvan = y_unvan
    WHERE kisi_id = y_kisi_id;

    UPDATE doktor
    SET uzmanlik = y_uzmanlik, bolum_id = y_bolum_id
    WHERE kisi_id = y_kisi_id;

    ELSE
        RAISE NOTICE 'Bu doktor ID % mevcut değildir.', y_kisi_id;
    END IF;
END;
$$;


ALTER PROCEDURE public.doktor_guncelleme(IN y_kisi_id integer, IN y_ad character varying, IN y_soyad character varying, IN y_dogum_tarihi date, IN y_cinsiyet character, IN y_ise_alinma_tarihi date, IN y_unvan character varying, IN y_uzmanlik character varying, IN y_bolum_id integer, IN y_telefon character, IN y_adres text, IN y_e_posta character varying) OWNER TO postgres;

--
-- TOC entry 269 (class 1255 OID 17325)
-- Name: doktorlar_yazdir(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.doktorlar_yazdir() RETURNS TABLE(id integer, "adı" text, "Doğum Tarihi" date, cinsiyet character, telefon character, adres text, e_posta character varying, "İşe alınma tarihi" date, "Ünvan" character varying, uzmanlik character varying, "Bölüm" character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT doktor.kisi_id AS "ID",
           CONCAT(kisi.ad,' ', kisi.soyad) AS "Adı",
           kisi.dogum_tarihi AS "Doğum Tarihi",
           kisi.cinsiyet,
           iletisim_bilgileri.telefon,
           iletisim_bilgileri.adres,
           iletisim_bilgileri.e_posta,
           calisan.ise_alinma_tarihi AS "İşe alınma tarihi",
           calisan.unvan AS "Ünvan",
           doktor.uzmanlik,
           bolum.ad AS "Bölüm"
    FROM doktor
    NATURAL JOIN calisan NATURAL JOIN kisi
    INNER JOIN iletisim_bilgileri ON kisi.iletisim_id = iletisim_bilgileri.id
    INNER JOIN bolum ON doktor.bolum_id = bolum.id;

END;
$$;


ALTER FUNCTION public.doktorlar_yazdir() OWNER TO postgres;

--
-- TOC entry 244 (class 1255 OID 17226)
-- Name: fatura_ekle(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.fatura_ekle() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    INSERT INTO fatura ("hasta_id", "detaylar", "topmlam")
    VALUES (NEW.hasta_id,'Randevu: 100TL', 100);
    RETURN NEW;
END;
$$;


ALTER FUNCTION public.fatura_ekle() OWNER TO postgres;

--
-- TOC entry 270 (class 1255 OID 17347)
-- Name: hasta_arama(integer, text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.hasta_arama(arnan_id integer DEFAULT NULL::integer, arnan_ad text DEFAULT NULL::text) RETURNS TABLE(id integer, "adı" text, "Doğum Tarihi" date, cinsiyet character, telefon character, adres text, e_posta character varying, kan_grubu character varying, sigorta_no integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT hasta.kisi_id AS "ID",
				   CONCAT(kisi.ad,' ', kisi.soyad) AS "Adı",
				   kisi.dogum_tarihi AS "Doğum Tarihi",
				   kisi.cinsiyet,
				   iletisim_bilgileri.telefon,
				   iletisim_bilgileri.adres,
				   iletisim_bilgileri.e_posta,
				   hasta.kan_grubu,
				   hasta.sigorta_no
    FROM hasta NATURAL JOIN kisi
    INNER JOIN iletisim_bilgileri ON kisi.iletisim_id = iletisim_bilgileri.id
    WHERE (arnan_id IS NULL OR hasta.kisi_id = arnan_id)
      AND (arnan_ad IS NULL OR CONCAT(kisi.ad, ' ', kisi.soyad) ILIKE '%' || arnan_ad || '%');
END;
$$;


ALTER FUNCTION public.hasta_arama(arnan_id integer, arnan_ad text) OWNER TO postgres;

--
-- TOC entry 275 (class 1255 OID 17354)
-- Name: hasta_ekle(character varying, character varying, date, character, character varying, integer, character, text, character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.hasta_ekle(IN yad character varying, IN ysoyad character varying, IN ydogum_tarihi date, IN ycinsiyet character, IN ykan_grubu character varying, IN ysigorta_no integer, IN ytelefon character, IN yadres text, IN ye_posta character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE
    iltsm_id INT;
    ykisi_id INT;
BEGIN
    INSERT INTO iletisim_bilgileri (telefon, adres, e_posta)
    VALUES (ytelefon, yadres, ye_posta) RETURNING id INTO iltsm_id;

    INSERT INTO kisi (ad, soyad, dogum_tarihi, cinsiyet, iletisim_id)
    VALUES (yad, ysoyad, ydogum_tarihi, ycinsiyet, iltsm_id)
    RETURNING kisi_id INTO ykisi_id;

    IF ysigorta_no = 0 THEN
        ysigorta_no := NULL;
    END IF;

    INSERT INTO hasta (kisi_id, kan_grubu, sigorta_no)
    VALUES (ykisi_id, ykan_grubu, ysigorta_no);
END;
$$;


ALTER PROCEDURE public.hasta_ekle(IN yad character varying, IN ysoyad character varying, IN ydogum_tarihi date, IN ycinsiyet character, IN ykan_grubu character varying, IN ysigorta_no integer, IN ytelefon character, IN yadres text, IN ye_posta character varying) OWNER TO postgres;

--
-- TOC entry 263 (class 1255 OID 17345)
-- Name: hasta_guncelleme(integer, character varying, character varying, date, character, character varying, character, text, character varying, integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.hasta_guncelleme(IN y_kisi_id integer, IN y_ad character varying, IN y_soyad character varying, IN y_dogum_tarihi date, IN y_cinsiyet character, IN y_kan_grubu character varying, IN y_telefon character, IN y_adres text, IN y_e_posta character varying, IN y_sigorta_no integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    IF EXISTS (SELECT 1 FROM hasta WHERE kisi_id = y_kisi_id) THEN

        UPDATE iletisim_bilgileri
        SET telefon = y_telefon, adres = y_adres, e_posta = y_e_posta
        WHERE id = (SELECT iletisim_id FROM kisi WHERE kisi_id = y_kisi_id);

        UPDATE kisi
        SET ad = y_ad, soyad = y_soyad, dogum_tarihi = y_dogum_tarihi, cinsiyet = y_cinsiyet
        WHERE kisi_id = y_kisi_id;
        
        IF y_sigorta_no = 0 THEN
            y_sigorta_no := NULL;
        END IF;
        
        UPDATE hasta
        SET kan_grubu = y_kan_grubu, sigorta_no = y_sigorta_no
        WHERE kisi_id = y_kisi_id;

    ELSE
        RAISE NOTICE 'Bu hasta ID % mevcut değildir.', y_kisi_id;
    END IF;
END;
$$;


ALTER PROCEDURE public.hasta_guncelleme(IN y_kisi_id integer, IN y_ad character varying, IN y_soyad character varying, IN y_dogum_tarihi date, IN y_cinsiyet character, IN y_kan_grubu character varying, IN y_telefon character, IN y_adres text, IN y_e_posta character varying, IN y_sigorta_no integer) OWNER TO postgres;

--
-- TOC entry 265 (class 1255 OID 17335)
-- Name: hasta_silme(integer); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.hasta_silme(IN hasta_id integer)
    LANGUAGE plpgsql
    AS $$
DECLARE
    iltsm_id INT;
BEGIN
    IF EXISTS (SELECT 1 FROM hasta WHERE kisi_id = hasta_id) THEN

        SELECT kisi.iletisim_id INTO iltsm_id
        FROM kisi
        WHERE kisi_id = hasta_id;

        DELETE FROM hasta WHERE kisi_id = hasta_id;

        DELETE FROM kisi WHERE kisi_id = hasta_id;

        IF NOT EXISTS (SELECT 1 FROM kisi WHERE iletisim_id = iltsm_id) THEN
            DELETE FROM iletisim_bilgileri WHERE id = iltsm_id;
        END IF;

    ELSE
        RAISE NOTICE 'Bu hasta ID % mevcut değildir.', hasta_id;
    END IF;
END;
$$;


ALTER PROCEDURE public.hasta_silme(IN hasta_id integer) OWNER TO postgres;

--
-- TOC entry 262 (class 1255 OID 17346)
-- Name: hastalar_yazdir(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.hastalar_yazdir() RETURNS TABLE(id integer, "adı" text, "Doğum Tarihi" date, cinsiyet character, telefon character, adres text, e_posta character varying, kan_grubu character varying, sigorta_no integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT hasta.kisi_id AS "ID",
				   CONCAT(kisi.ad,' ', kisi.soyad) AS "Adı",
				   kisi.dogum_tarihi AS "Doğum Tarihi",
				   kisi.cinsiyet,
				   iletisim_bilgileri.telefon,
				   iletisim_bilgileri.adres,
				   iletisim_bilgileri.e_posta,
				   hasta.kan_grubu,
				   hasta.sigorta_no
    FROM hasta NATURAL JOIN kisi
    INNER JOIN iletisim_bilgileri ON kisi.iletisim_id = iletisim_bilgileri.id;
END;
$$;


ALTER FUNCTION public.hastalar_yazdir() OWNER TO postgres;

--
-- TOC entry 268 (class 1255 OID 17331)
-- Name: hemsire_arama(integer, text); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.hemsire_arama(arnan_id integer DEFAULT NULL::integer, arnan_ad text DEFAULT NULL::text) RETURNS TABLE(id integer, "adı" text, "Doğum Tarihi" date, cinsiyet character, telefon character, adres text, e_posta character varying, "İşe alınma tarihi" date, "Ünvan" character varying, "Bölüm" character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT hemsire.kisi_id AS "ID",
           CONCAT(kisi.ad,' ', kisi.soyad) AS "Adı",
           kisi.dogum_tarihi AS "Doğum Tarihi",
           kisi.cinsiyet,
           iletisim_bilgileri.telefon,
           iletisim_bilgileri.adres,
           iletisim_bilgileri.e_posta,
           calisan.ise_alinma_tarihi AS "İşe alınma tarihi",
           calisan.unvan AS "Ünvan",
           bolum.ad AS "Bölüm"
    FROM hemsire
    NATURAL JOIN calisan NATURAL JOIN kisi
    INNER JOIN iletisim_bilgileri ON kisi.iletisim_id = iletisim_bilgileri.id
    INNER JOIN bolum ON hemsire.bolum_id = bolum.id
    WHERE (arnan_id IS NULL OR hemsire.kisi_id = arnan_id)
      AND (arnan_ad IS NULL OR CONCAT(kisi.ad, ' ', kisi.soyad) ILIKE '%' || arnan_ad || '%');
END;
$$;


ALTER FUNCTION public.hemsire_arama(arnan_id integer, arnan_ad text) OWNER TO postgres;

--
-- TOC entry 258 (class 1255 OID 17314)
-- Name: hemsire_ekle(character varying, character varying, date, character, date, character varying, integer, character, text, character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.hemsire_ekle(IN yad character varying, IN ysoyad character varying, IN ydogum_tarihi date, IN ycinsiyet character, IN yise_alma_tarihi date, IN yunvan character varying, IN ybolum_id integer, IN ytelefon character, IN yadres text, IN ye_posta character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE
    iltsm_id INT;
    ykisi_id INT;
BEGIN
    INSERT INTO iletisim_bilgileri (telefon, adres, e_posta)
    VALUES (ytelefon, yadres, ye_posta) RETURNING id INTO iltsm_id;

    INSERT INTO kisi (ad, soyad, dogum_tarihi, cinsiyet, iletisim_id)
    VALUES (yad, ysoyad, ydogum_tarihi, ycinsiyet, iltsm_id)
    RETURNING kisi_id INTO ykisi_id;

    INSERT INTO calisan (kisi_id, ise_alinma_tarihi, unvan)
    VALUES (ykisi_id, yise_alma_tarihi, yunvan);

    INSERT INTO hemsire (kisi_id, bolum_id)
    VALUES (ykisi_id, ybolum_id);
END;
$$;


ALTER PROCEDURE public.hemsire_ekle(IN yad character varying, IN ysoyad character varying, IN ydogum_tarihi date, IN ycinsiyet character, IN yise_alma_tarihi date, IN yunvan character varying, IN ybolum_id integer, IN ytelefon character, IN yadres text, IN ye_posta character varying) OWNER TO postgres;

--
-- TOC entry 272 (class 1255 OID 17349)
-- Name: hemsire_guncelleme(integer, character varying, character varying, date, character, date, character varying, integer, character, text, character varying); Type: PROCEDURE; Schema: public; Owner: postgres
--

CREATE PROCEDURE public.hemsire_guncelleme(IN y_kisi_id integer, IN y_ad character varying, IN y_soyad character varying, IN y_dogum_tarihi date, IN y_cinsiyet character, IN y_ise_alinma_tarihi date, IN y_unvan character varying, IN y_bolum_id integer, IN y_telefon character, IN y_adres text, IN y_e_posta character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    IF EXISTS (SELECT 1 FROM hemsire WHERE kisi_id = y_kisi_id) THEN

        UPDATE iletisim_bilgileri
        SET telefon = y_telefon, adres = y_adres, e_posta = y_e_posta
        WHERE id = (SELECT iletisim_id FROM kisi WHERE kisi_id = y_kisi_id);

        UPDATE kisi
        SET ad = y_ad, soyad = y_soyad, dogum_tarihi = y_dogum_tarihi, cinsiyet = y_cinsiyet
        WHERE kisi_id = y_kisi_id;

        UPDATE calisan
        SET ise_alinma_tarihi = y_ise_alinma_tarihi, unvan = y_unvan
        WHERE kisi_id = y_kisi_id;

        UPDATE hemsire
        SET bolum_id = y_bolum_id
        WHERE kisi_id = y_kisi_id;

    ELSE
        RAISE NOTICE 'Bu hemsire ID % mevcut değildir.', y_kisi_id;
    END IF;
END;
$$;


ALTER PROCEDURE public.hemsire_guncelleme(IN y_kisi_id integer, IN y_ad character varying, IN y_soyad character varying, IN y_dogum_tarihi date, IN y_cinsiyet character, IN y_ise_alinma_tarihi date, IN y_unvan character varying, IN y_bolum_id integer, IN y_telefon character, IN y_adres text, IN y_e_posta character varying) OWNER TO postgres;

--
-- TOC entry 260 (class 1255 OID 17326)
-- Name: hemsireler_yazdir(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.hemsireler_yazdir() RETURNS TABLE(id integer, "adı" text, "Doğum Tarihi" date, cinsiyet character, telefon character, adres text, e_posta character varying, "İşe alınma tarihi" date, "Ünvan" character varying, "Bölüm" character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
    RETURN QUERY
    SELECT hemsire.kisi_id AS "ID",
           CONCAT(kisi.ad,' ', kisi.soyad) AS "Adı",
           kisi.dogum_tarihi AS "Doğum Tarihi",
           kisi.cinsiyet,
           iletisim_bilgileri.telefon,
           iletisim_bilgileri.adres,
           iletisim_bilgileri.e_posta,
           calisan.ise_alinma_tarihi AS "İşe alınma tarihi",
           calisan.unvan AS "Ünvan",
           bolum.ad AS "Bölüm"
    FROM hemsire
    NATURAL JOIN calisan NATURAL JOIN kisi
    INNER JOIN iletisim_bilgileri ON kisi.iletisim_id = iletisim_bilgileri.id
    INNER JOIN bolum ON hemsire.bolum_id = bolum.id;

END;
$$;


ALTER FUNCTION public.hemsireler_yazdir() OWNER TO postgres;

--
-- TOC entry 273 (class 1255 OID 17350)
-- Name: ilac_icin_fatura_guncelleme(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.ilac_icin_fatura_guncelleme() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    IF EXISTS (SELECT 1 FROM fatura WHERE hasta_id = (SELECT hasta_id FROM tibbi_kayit
               WHERE id = (SELECT tibbi_kayit_id FROM recete WHERE id = NEW.recete_id))) THEN

        UPDATE fatura
        SET topmlam = topmlam + (NEW.maliyet - COALESCE(OLD.maliyet, 0)),
            detaylar = detaylar || ' | İlaç: ' || NEW.adi || ' (' || NEW.maliyet || ' TL)'
        WHERE hasta_id = (SELECT hasta_id FROM tibbi_kayit WHERE id = (SELECT tibbi_kayit_id FROM recete WHERE id = NEW.recete_id));
    ELSE

        INSERT INTO fatura (hasta_id, detaylar, topmlam)
        VALUES (
            (SELECT hasta_id FROM tibbi_kayit WHERE id = (SELECT tibbi_kayit_id FROM recete WHERE id = NEW.recete_id)),
            ' | İlaç: ' || NEW.adi || ' (' || NEW.maliyet || ' TL)\n',
            NEW.maliyet
        );
    END IF;
    RETURN NEW;
END;
$$;


ALTER FUNCTION public.ilac_icin_fatura_guncelleme() OWNER TO postgres;

--
-- TOC entry 274 (class 1255 OID 17352)
-- Name: lab_testi_icin_fatura_guncelleme(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.lab_testi_icin_fatura_guncelleme() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    IF EXISTS (SELECT 1 FROM fatura WHERE hasta_id = NEW.hasta_id) THEN

        UPDATE fatura
        SET topmlam = topmlam + (NEW.maliyet - COALESCE(OLD.maliyet, 0)),
            detaylar = detaylar || ' | Lab testi: ' || NEW.test_turu || ' (' || NEW.maliyet || ' TL )'
        WHERE hasta_id = NEW.hasta_id;

    ELSE
        INSERT INTO fatura (hasta_id, detaylar, topmlam)
        VALUES (NEW.hasta_id, 'Lab Test Fiyatı: ', NEW.maliyet);
    END IF;
    RETURN NEW;
END;
$$;


ALTER FUNCTION public.lab_testi_icin_fatura_guncelleme() OWNER TO postgres;

--
-- TOC entry 243 (class 1255 OID 17150)
-- Name: radndevu_cikisma_kontrolu(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.radndevu_cikisma_kontrolu() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    IF EXISTS (
        SELECT 1
        FROM randevu
        WHERE doktor_id = NEW.doktor_id
          AND tarih::DATE = NEW.tarih::DATE
          AND NEW.tarih::TIME BETWEEN randevu.tarih::TIME AND randevu.tarih::TIME + INTERVAL'30 MINUTES'
    ) THEN
        RAISE EXCEPTION 'Doktor % için randevu çakışması vardir',
            NEW.doktor_id;
    END IF;
    RETURN NEW;
END;
$$;


ALTER FUNCTION public.radndevu_cikisma_kontrolu() OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 226 (class 1259 OID 17044)
-- Name: bolum; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.bolum (
    id integer NOT NULL,
    ad character varying(100) NOT NULL,
    bolum_baskani character varying(100)
);


ALTER TABLE public.bolum OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 17043)
-- Name: bolum_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.bolum_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.bolum_id_seq OWNER TO postgres;

--
-- TOC entry 4972 (class 0 OID 0)
-- Dependencies: 225
-- Name: bolum_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.bolum_id_seq OWNED BY public.bolum.id;


--
-- TOC entry 221 (class 1259 OID 17011)
-- Name: calisan; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.calisan (
    kisi_id integer NOT NULL,
    ise_alinma_tarihi date NOT NULL,
    unvan character varying(40) NOT NULL
);


ALTER TABLE public.calisan OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 17033)
-- Name: doktor; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.doktor (
    kisi_id integer NOT NULL,
    uzmanlik character varying(100) NOT NULL,
    bolum_id integer
);


ALTER TABLE public.doktor OWNER TO postgres;

--
-- TOC entry 242 (class 1259 OID 17212)
-- Name: fatura; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.fatura (
    id integer NOT NULL,
    hasta_id integer NOT NULL,
    detaylar text NOT NULL,
    topmlam numeric DEFAULT 100
);


ALTER TABLE public.fatura OWNER TO postgres;

--
-- TOC entry 241 (class 1259 OID 17211)
-- Name: fatura_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.fatura_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.fatura_id_seq OWNER TO postgres;

--
-- TOC entry 4973 (class 0 OID 0)
-- Dependencies: 241
-- Name: fatura_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.fatura_id_seq OWNED BY public.fatura.id;


--
-- TOC entry 230 (class 1259 OID 17089)
-- Name: hasta; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.hasta (
    kisi_id integer NOT NULL,
    kan_grubu character varying(3) NOT NULL,
    sigorta_no integer
);


ALTER TABLE public.hasta OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 17067)
-- Name: hemsire; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.hemsire (
    kisi_id integer NOT NULL,
    bolum_id integer
);


ALTER TABLE public.hemsire OWNER TO postgres;

--
-- TOC entry 238 (class 1259 OID 17181)
-- Name: ilac; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.ilac (
    id integer NOT NULL,
    recete_id integer NOT NULL,
    adi character varying(50) NOT NULL,
    baslangic_tarihi date NOT NULL,
    bitis_tarihi date NOT NULL,
    maliyet numeric(10,2) NOT NULL
);


ALTER TABLE public.ilac OWNER TO postgres;

--
-- TOC entry 237 (class 1259 OID 17180)
-- Name: ilac_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.ilac_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.ilac_id_seq OWNER TO postgres;

--
-- TOC entry 4974 (class 0 OID 0)
-- Dependencies: 237
-- Name: ilac_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.ilac_id_seq OWNED BY public.ilac.id;


--
-- TOC entry 218 (class 1259 OID 16987)
-- Name: iletisim_bilgileri; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.iletisim_bilgileri (
    id integer NOT NULL,
    telefon character(11) NOT NULL,
    adres text NOT NULL,
    e_posta character varying(150)
);


ALTER TABLE public.iletisim_bilgileri OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16986)
-- Name: iletisim_bilgileri_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.iletisim_bilgileri_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.iletisim_bilgileri_id_seq OWNER TO postgres;

--
-- TOC entry 4975 (class 0 OID 0)
-- Dependencies: 217
-- Name: iletisim_bilgileri_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.iletisim_bilgileri_id_seq OWNED BY public.iletisim_bilgileri.id;


--
-- TOC entry 220 (class 1259 OID 16998)
-- Name: kisi; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.kisi (
    kisi_id integer NOT NULL,
    ad character varying(50) NOT NULL,
    soyad character varying(50) NOT NULL,
    dogum_tarihi date NOT NULL,
    cinsiyet character(5) NOT NULL,
    iletisim_id integer
);


ALTER TABLE public.kisi OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16997)
-- Name: kisi_kisi_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.kisi_kisi_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.kisi_kisi_id_seq OWNER TO postgres;

--
-- TOC entry 4976 (class 0 OID 0)
-- Dependencies: 219
-- Name: kisi_kisi_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.kisi_kisi_id_seq OWNED BY public.kisi.kisi_id;


--
-- TOC entry 240 (class 1259 OID 17193)
-- Name: lab_testi; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.lab_testi (
    id integer NOT NULL,
    hasta_id integer NOT NULL,
    doktor_id integer NOT NULL,
    test_turu character varying(50) NOT NULL,
    tarih date NOT NULL,
    sonucu text NOT NULL,
    maliyet numeric(10,2) NOT NULL
);


ALTER TABLE public.lab_testi OWNER TO postgres;

--
-- TOC entry 239 (class 1259 OID 17192)
-- Name: lab_testi_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.lab_testi_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.lab_testi_id_seq OWNER TO postgres;

--
-- TOC entry 4977 (class 0 OID 0)
-- Dependencies: 239
-- Name: lab_testi_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.lab_testi_id_seq OWNED BY public.lab_testi.id;


--
-- TOC entry 232 (class 1259 OID 17132)
-- Name: randevu; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.randevu (
    id integer NOT NULL,
    hasta_id integer NOT NULL,
    doktor_id integer NOT NULL,
    tarih timestamp without time zone NOT NULL,
    sebap text NOT NULL
);


ALTER TABLE public.randevu OWNER TO postgres;

--
-- TOC entry 231 (class 1259 OID 17131)
-- Name: randevu_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.randevu_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.randevu_id_seq OWNER TO postgres;

--
-- TOC entry 4978 (class 0 OID 0)
-- Dependencies: 231
-- Name: randevu_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.randevu_id_seq OWNED BY public.randevu.id;


--
-- TOC entry 236 (class 1259 OID 17167)
-- Name: recete; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.recete (
    id integer NOT NULL,
    tibbi_kayit_id bigint NOT NULL,
    dozaj character varying(50) NOT NULL,
    talimatlar text NOT NULL
);


ALTER TABLE public.recete OWNER TO postgres;

--
-- TOC entry 235 (class 1259 OID 17166)
-- Name: recete_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.recete_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.recete_id_seq OWNER TO postgres;

--
-- TOC entry 4979 (class 0 OID 0)
-- Dependencies: 235
-- Name: recete_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.recete_id_seq OWNED BY public.recete.id;


--
-- TOC entry 229 (class 1259 OID 17083)
-- Name: sigorta; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sigorta (
    id integer NOT NULL,
    police_detaylari character varying(50) NOT NULL
);


ALTER TABLE public.sigorta OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 17082)
-- Name: sigorta_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.sigorta_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.sigorta_id_seq OWNER TO postgres;

--
-- TOC entry 4980 (class 0 OID 0)
-- Dependencies: 228
-- Name: sigorta_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.sigorta_id_seq OWNED BY public.sigorta.id;


--
-- TOC entry 234 (class 1259 OID 17153)
-- Name: tibbi_kayit; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tibbi_kayit (
    id bigint NOT NULL,
    hasta_id integer NOT NULL,
    olustuma_tarihi date NOT NULL,
    teshis text NOT NULL,
    tedavi_plani text NOT NULL
);


ALTER TABLE public.tibbi_kayit OWNER TO postgres;

--
-- TOC entry 233 (class 1259 OID 17152)
-- Name: tibbi_kayit_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tibbi_kayit_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.tibbi_kayit_id_seq OWNER TO postgres;

--
-- TOC entry 4981 (class 0 OID 0)
-- Dependencies: 233
-- Name: tibbi_kayit_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tibbi_kayit_id_seq OWNED BY public.tibbi_kayit.id;


--
-- TOC entry 223 (class 1259 OID 17022)
-- Name: vardiya; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.vardiya (
    id integer NOT NULL,
    calisan_id integer NOT NULL,
    tarih date NOT NULL,
    baslangic_saati time without time zone NOT NULL,
    bitis_saati time without time zone NOT NULL
);


ALTER TABLE public.vardiya OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 17021)
-- Name: vardiya_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.vardiya_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.vardiya_id_seq OWNER TO postgres;

--
-- TOC entry 4982 (class 0 OID 0)
-- Dependencies: 222
-- Name: vardiya_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.vardiya_id_seq OWNED BY public.vardiya.id;


--
-- TOC entry 4732 (class 2604 OID 17047)
-- Name: bolum id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bolum ALTER COLUMN id SET DEFAULT nextval('public.bolum_id_seq'::regclass);


--
-- TOC entry 4739 (class 2604 OID 17215)
-- Name: fatura id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fatura ALTER COLUMN id SET DEFAULT nextval('public.fatura_id_seq'::regclass);


--
-- TOC entry 4737 (class 2604 OID 17184)
-- Name: ilac id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ilac ALTER COLUMN id SET DEFAULT nextval('public.ilac_id_seq'::regclass);


--
-- TOC entry 4729 (class 2604 OID 16990)
-- Name: iletisim_bilgileri id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.iletisim_bilgileri ALTER COLUMN id SET DEFAULT nextval('public.iletisim_bilgileri_id_seq'::regclass);


--
-- TOC entry 4730 (class 2604 OID 17001)
-- Name: kisi kisi_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kisi ALTER COLUMN kisi_id SET DEFAULT nextval('public.kisi_kisi_id_seq'::regclass);


--
-- TOC entry 4738 (class 2604 OID 17196)
-- Name: lab_testi id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lab_testi ALTER COLUMN id SET DEFAULT nextval('public.lab_testi_id_seq'::regclass);


--
-- TOC entry 4734 (class 2604 OID 17135)
-- Name: randevu id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.randevu ALTER COLUMN id SET DEFAULT nextval('public.randevu_id_seq'::regclass);


--
-- TOC entry 4736 (class 2604 OID 17170)
-- Name: recete id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recete ALTER COLUMN id SET DEFAULT nextval('public.recete_id_seq'::regclass);


--
-- TOC entry 4733 (class 2604 OID 17086)
-- Name: sigorta id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sigorta ALTER COLUMN id SET DEFAULT nextval('public.sigorta_id_seq'::regclass);


--
-- TOC entry 4735 (class 2604 OID 17156)
-- Name: tibbi_kayit id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tibbi_kayit ALTER COLUMN id SET DEFAULT nextval('public.tibbi_kayit_id_seq'::regclass);


--
-- TOC entry 4731 (class 2604 OID 17025)
-- Name: vardiya id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vardiya ALTER COLUMN id SET DEFAULT nextval('public.vardiya_id_seq'::regclass);


--
-- TOC entry 4950 (class 0 OID 17044)
-- Dependencies: 226
-- Data for Name: bolum; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.bolum (id, ad, bolum_baskani) FROM stdin;
1	Beyin ve Sinir	Mehmet Yılmaz
\.


--
-- TOC entry 4945 (class 0 OID 17011)
-- Dependencies: 221
-- Data for Name: calisan; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.calisan (kisi_id, ise_alinma_tarihi, unvan) FROM stdin;
7	2000-01-02	Uzmanalti
8	2009-09-05	Prof. Dr
9	2020-09-07	Öğremci
2	2012-12-12	Doktor
\.


--
-- TOC entry 4948 (class 0 OID 17033)
-- Dependencies: 224
-- Data for Name: doktor; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.doktor (kisi_id, uzmanlik, bolum_id) FROM stdin;
8	Genel Cerrah	1
\.


--
-- TOC entry 4966 (class 0 OID 17212)
-- Dependencies: 242
-- Data for Name: fatura; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.fatura (id, hasta_id, detaylar, topmlam) FROM stdin;
\.


--
-- TOC entry 4954 (class 0 OID 17089)
-- Dependencies: 230
-- Data for Name: hasta; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.hasta (kisi_id, kan_grubu, sigorta_no) FROM stdin;
11	A-	2
21	O-	\N
\.


--
-- TOC entry 4951 (class 0 OID 17067)
-- Dependencies: 227
-- Data for Name: hemsire; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.hemsire (kisi_id, bolum_id) FROM stdin;
9	1
\.


--
-- TOC entry 4962 (class 0 OID 17181)
-- Dependencies: 238
-- Data for Name: ilac; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.ilac (id, recete_id, adi, baslangic_tarihi, bitis_tarihi, maliyet) FROM stdin;
\.


--
-- TOC entry 4942 (class 0 OID 16987)
-- Dependencies: 218
-- Data for Name: iletisim_bilgileri; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.iletisim_bilgileri (id, telefon, adres, e_posta) FROM stdin;
10	05512324232	Turkiye/İstanbul	ahmet@dsa.edu.tr
11	05123123144	Turkiye/Sakarya	mehmet@sda.tr
12	05123321333	Turkiye/Ankara	gemze@gm.tr
14	05123123111	Test	ahmet@tr
5	05555555551	sakary	ahmeqwt@sda
32	05511122233	Sakarya	bilal.alsayad@edu.tr
\.


--
-- TOC entry 4944 (class 0 OID 16998)
-- Dependencies: 220
-- Data for Name: kisi; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.kisi (kisi_id, ad, soyad, dogum_tarihi, cinsiyet, iletisim_id) FROM stdin;
7	Ahmet	Rabee	2002-10-05	ERKEK	10
8	Mehmet	Yilmez	1988-10-07	ERKEK	11
9	Gemze	Yildiz	1997-11-30	Kadın	12
11	Ahmet	sda	2024-12-23	Erkek	14
2	Ahmet	Ahmet	1995-01-02	Eekek	5
21	Ahmet	Alsayad	2002-05-30	Erkek	32
\.


--
-- TOC entry 4964 (class 0 OID 17193)
-- Dependencies: 240
-- Data for Name: lab_testi; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.lab_testi (id, hasta_id, doktor_id, test_turu, tarih, sonucu, maliyet) FROM stdin;
\.


--
-- TOC entry 4956 (class 0 OID 17132)
-- Dependencies: 232
-- Data for Name: randevu; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.randevu (id, hasta_id, doktor_id, tarih, sebap) FROM stdin;
\.


--
-- TOC entry 4960 (class 0 OID 17167)
-- Dependencies: 236
-- Data for Name: recete; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.recete (id, tibbi_kayit_id, dozaj, talimatlar) FROM stdin;
\.


--
-- TOC entry 4953 (class 0 OID 17083)
-- Dependencies: 229
-- Data for Name: sigorta; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.sigorta (id, police_detaylari) FROM stdin;
2	23112
3	123321
6	1321
7	das1
4	dassa1
\.


--
-- TOC entry 4958 (class 0 OID 17153)
-- Dependencies: 234
-- Data for Name: tibbi_kayit; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tibbi_kayit (id, hasta_id, olustuma_tarihi, teshis, tedavi_plani) FROM stdin;
\.


--
-- TOC entry 4947 (class 0 OID 17022)
-- Dependencies: 223
-- Data for Name: vardiya; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.vardiya (id, calisan_id, tarih, baslangic_saati, bitis_saati) FROM stdin;
2	2	2024-12-24	18:00:37	00:00:37
\.


--
-- TOC entry 4983 (class 0 OID 0)
-- Dependencies: 225
-- Name: bolum_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.bolum_id_seq', 2, true);


--
-- TOC entry 4984 (class 0 OID 0)
-- Dependencies: 241
-- Name: fatura_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.fatura_id_seq', 3, true);


--
-- TOC entry 4985 (class 0 OID 0)
-- Dependencies: 237
-- Name: ilac_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.ilac_id_seq', 3, true);


--
-- TOC entry 4986 (class 0 OID 0)
-- Dependencies: 217
-- Name: iletisim_bilgileri_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.iletisim_bilgileri_id_seq', 32, true);


--
-- TOC entry 4987 (class 0 OID 0)
-- Dependencies: 219
-- Name: kisi_kisi_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.kisi_kisi_id_seq', 21, true);


--
-- TOC entry 4988 (class 0 OID 0)
-- Dependencies: 239
-- Name: lab_testi_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.lab_testi_id_seq', 4, true);


--
-- TOC entry 4989 (class 0 OID 0)
-- Dependencies: 231
-- Name: randevu_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.randevu_id_seq', 3, true);


--
-- TOC entry 4990 (class 0 OID 0)
-- Dependencies: 235
-- Name: recete_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.recete_id_seq', 2, true);


--
-- TOC entry 4991 (class 0 OID 0)
-- Dependencies: 228
-- Name: sigorta_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.sigorta_id_seq', 7, true);


--
-- TOC entry 4992 (class 0 OID 0)
-- Dependencies: 233
-- Name: tibbi_kayit_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tibbi_kayit_id_seq', 2, true);


--
-- TOC entry 4993 (class 0 OID 0)
-- Dependencies: 222
-- Name: vardiya_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.vardiya_id_seq', 2, true);


--
-- TOC entry 4756 (class 2606 OID 17049)
-- Name: bolum bolum_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bolum
    ADD CONSTRAINT bolum_pkey PRIMARY KEY (id);


--
-- TOC entry 4750 (class 2606 OID 17015)
-- Name: calisan calisan_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.calisan
    ADD CONSTRAINT calisan_pkey PRIMARY KEY (kisi_id);


--
-- TOC entry 4754 (class 2606 OID 17037)
-- Name: doktor doktor_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.doktor
    ADD CONSTRAINT doktor_pkey PRIMARY KEY (kisi_id);


--
-- TOC entry 4774 (class 2606 OID 17220)
-- Name: fatura fatura_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fatura
    ADD CONSTRAINT fatura_pkey PRIMARY KEY (id);


--
-- TOC entry 4762 (class 2606 OID 17093)
-- Name: hasta hasta_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hasta
    ADD CONSTRAINT hasta_pkey PRIMARY KEY (kisi_id);


--
-- TOC entry 4758 (class 2606 OID 17071)
-- Name: hemsire hemsire_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hemsire
    ADD CONSTRAINT hemsire_pkey PRIMARY KEY (kisi_id);


--
-- TOC entry 4770 (class 2606 OID 17186)
-- Name: ilac ilac_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ilac
    ADD CONSTRAINT ilac_pkey PRIMARY KEY (id);


--
-- TOC entry 4742 (class 2606 OID 16996)
-- Name: iletisim_bilgileri iletisim_bilgileri_e_posta_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.iletisim_bilgileri
    ADD CONSTRAINT iletisim_bilgileri_e_posta_key UNIQUE (e_posta);


--
-- TOC entry 4744 (class 2606 OID 16994)
-- Name: iletisim_bilgileri iletisim_bilgileri_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.iletisim_bilgileri
    ADD CONSTRAINT iletisim_bilgileri_pkey PRIMARY KEY (id);


--
-- TOC entry 4746 (class 2606 OID 17005)
-- Name: kisi iletisim_id_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kisi
    ADD CONSTRAINT iletisim_id_unique UNIQUE (iletisim_id);


--
-- TOC entry 4748 (class 2606 OID 17003)
-- Name: kisi kisi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kisi
    ADD CONSTRAINT kisi_pkey PRIMARY KEY (kisi_id);


--
-- TOC entry 4772 (class 2606 OID 17200)
-- Name: lab_testi lab_testi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lab_testi
    ADD CONSTRAINT lab_testi_pkey PRIMARY KEY (id);


--
-- TOC entry 4764 (class 2606 OID 17139)
-- Name: randevu randevu_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.randevu
    ADD CONSTRAINT randevu_pkey PRIMARY KEY (id);


--
-- TOC entry 4768 (class 2606 OID 17174)
-- Name: recete recete_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recete
    ADD CONSTRAINT recete_pkey PRIMARY KEY (id);


--
-- TOC entry 4760 (class 2606 OID 17088)
-- Name: sigorta sigorta_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sigorta
    ADD CONSTRAINT sigorta_pkey PRIMARY KEY (id);


--
-- TOC entry 4766 (class 2606 OID 17160)
-- Name: tibbi_kayit tibbi_kayit_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tibbi_kayit
    ADD CONSTRAINT tibbi_kayit_pkey PRIMARY KEY (id);


--
-- TOC entry 4752 (class 2606 OID 17027)
-- Name: vardiya vardiya_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vardiya
    ADD CONSTRAINT vardiya_pkey PRIMARY KEY (id);


--
-- TOC entry 4792 (class 2620 OID 17151)
-- Name: randevu radndevu_cikisma; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER radndevu_cikisma BEFORE INSERT OR UPDATE ON public.randevu FOR EACH ROW EXECUTE FUNCTION public.radndevu_cikisma_kontrolu();


--
-- TOC entry 4793 (class 2620 OID 17227)
-- Name: randevu randevu_olusturuldugunda_fatura_ekle; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER randevu_olusturuldugunda_fatura_ekle AFTER INSERT ON public.randevu FOR EACH ROW EXECUTE FUNCTION public.fatura_ekle();


--
-- TOC entry 4794 (class 2620 OID 17351)
-- Name: ilac tg_ilac_icin_fatura_guncelleme; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER tg_ilac_icin_fatura_guncelleme AFTER INSERT OR UPDATE ON public.ilac FOR EACH ROW EXECUTE FUNCTION public.ilac_icin_fatura_guncelleme();


--
-- TOC entry 4795 (class 2620 OID 17353)
-- Name: lab_testi trg_lab_testi_icin_fatura_guncelleme; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_lab_testi_icin_fatura_guncelleme AFTER INSERT OR UPDATE ON public.lab_testi FOR EACH ROW EXECUTE FUNCTION public.lab_testi_icin_fatura_guncelleme();


--
-- TOC entry 4776 (class 2606 OID 17016)
-- Name: calisan calisan_kisi_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.calisan
    ADD CONSTRAINT calisan_kisi_id_fkey FOREIGN KEY (kisi_id) REFERENCES public.kisi(kisi_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4778 (class 2606 OID 17062)
-- Name: doktor doktor_bolum_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.doktor
    ADD CONSTRAINT doktor_bolum_id_fkey FOREIGN KEY (bolum_id) REFERENCES public.bolum(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- TOC entry 4779 (class 2606 OID 17038)
-- Name: doktor doktor_kisi_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.doktor
    ADD CONSTRAINT doktor_kisi_id_fkey FOREIGN KEY (kisi_id) REFERENCES public.calisan(kisi_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4791 (class 2606 OID 17221)
-- Name: fatura fatura_hasta_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fatura
    ADD CONSTRAINT fatura_hasta_id_fkey FOREIGN KEY (hasta_id) REFERENCES public.hasta(kisi_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4782 (class 2606 OID 17094)
-- Name: hasta hasta_kisi_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hasta
    ADD CONSTRAINT hasta_kisi_id_fkey FOREIGN KEY (kisi_id) REFERENCES public.kisi(kisi_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4783 (class 2606 OID 17099)
-- Name: hasta hasta_sigorta_no_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hasta
    ADD CONSTRAINT hasta_sigorta_no_fkey FOREIGN KEY (sigorta_no) REFERENCES public.sigorta(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- TOC entry 4780 (class 2606 OID 17077)
-- Name: hemsire hemsire_bolum_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hemsire
    ADD CONSTRAINT hemsire_bolum_id_fkey FOREIGN KEY (bolum_id) REFERENCES public.bolum(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- TOC entry 4781 (class 2606 OID 17072)
-- Name: hemsire hemsire_kisi_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.hemsire
    ADD CONSTRAINT hemsire_kisi_id_fkey FOREIGN KEY (kisi_id) REFERENCES public.calisan(kisi_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4788 (class 2606 OID 17187)
-- Name: ilac ilac_recete_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.ilac
    ADD CONSTRAINT ilac_recete_id_fkey FOREIGN KEY (recete_id) REFERENCES public.recete(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4775 (class 2606 OID 17006)
-- Name: kisi kisi_iletisim_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kisi
    ADD CONSTRAINT kisi_iletisim_id_fkey FOREIGN KEY (iletisim_id) REFERENCES public.iletisim_bilgileri(id) ON UPDATE CASCADE ON DELETE SET NULL;


--
-- TOC entry 4789 (class 2606 OID 17206)
-- Name: lab_testi lab_testi_doktor_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lab_testi
    ADD CONSTRAINT lab_testi_doktor_id_fkey FOREIGN KEY (doktor_id) REFERENCES public.doktor(kisi_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4790 (class 2606 OID 17201)
-- Name: lab_testi lab_testi_hasta_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lab_testi
    ADD CONSTRAINT lab_testi_hasta_id_fkey FOREIGN KEY (hasta_id) REFERENCES public.hasta(kisi_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4784 (class 2606 OID 17145)
-- Name: randevu randevu_doktor_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.randevu
    ADD CONSTRAINT randevu_doktor_id_fkey FOREIGN KEY (doktor_id) REFERENCES public.doktor(kisi_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4785 (class 2606 OID 17140)
-- Name: randevu randevu_hasta_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.randevu
    ADD CONSTRAINT randevu_hasta_id_fkey FOREIGN KEY (hasta_id) REFERENCES public.hasta(kisi_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4787 (class 2606 OID 17175)
-- Name: recete recete_tibbi_kayit_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.recete
    ADD CONSTRAINT recete_tibbi_kayit_id_fkey FOREIGN KEY (tibbi_kayit_id) REFERENCES public.tibbi_kayit(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4786 (class 2606 OID 17161)
-- Name: tibbi_kayit tibbi_kayit_hasta_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tibbi_kayit
    ADD CONSTRAINT tibbi_kayit_hasta_id_fkey FOREIGN KEY (hasta_id) REFERENCES public.hasta(kisi_id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- TOC entry 4777 (class 2606 OID 17028)
-- Name: vardiya vardiya_calisan_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vardiya
    ADD CONSTRAINT vardiya_calisan_id_fkey FOREIGN KEY (calisan_id) REFERENCES public.calisan(kisi_id) ON UPDATE CASCADE ON DELETE CASCADE;


-- Completed on 2024-12-24 20:36:13

--
-- PostgreSQL database dump complete
--

