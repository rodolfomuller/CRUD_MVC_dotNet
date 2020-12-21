/* 
Script inicial para dividir as informações do excel recebido para usar pelo banco de dados
-----------------------------------------------------------
CREATE TABLE public.regiao_UF
(
    id serial NOT NULL,
    nome character varying(100) COLLATE pg_catalog."default" NOT NULL,
	id_uf integer,
    CONSTRAINT "regiao_uf_pkey" PRIMARY KEY (id)
);

CREATE TABLE public.UF
(
    id serial NOT NULL,
    nome character varying(2) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "uf_pkey" PRIMARY KEY (id)
);

CREATE TABLE public.cidade
(
    id SERIAL NOT NULL,
    ibge integer NOT NULL,
	id_uf character varying(2),
	nome character varying(100) ,
	longitude character varying(18),
	latitude character varying(18),
	id_regiao_uf character varying(100),
    CONSTRAINT pk_cidade PRIMARY KEY (id),
	CONSTRAINT unq_ibge UNIQUE (ibge),
    CONSTRAINT unq_nome_uf UNIQUE (nome, id_uf)
);
	
CREATE TABLE public.cidade_aux
(
    id SERIAL NOT NULL,
    ibge integer NOT NULL,
	uf character varying(2),
	nome character varying(100) ,
	longitude character varying(18),
	latitude character varying(18),
	regiao character varying(100),
    CONSTRAINT pk_cidade_aux PRIMARY KEY (id)
);

INSERT INTO public.UF (nome)
SELECT DISTINCT cidade_aux.uf FROM cidade_aux 
ORDER BY cidade_aux.uf;

INSERT INTO public.regiao_UF (nome, id_uf)
SELECT distinct regiao, uf.id FROM cidade_aux 
INNER JOIN uf on uf.nome = cidade_aux.uf order by regiao;

INSERT INTO public.cidade( ibge, id_uf, nome, longitude, latitude, id_regiao_uf)
SELECT  ibge, uf.id, cidade_aux.nome, longitude, latitude, regiao_uf.id FROM cidade_aux 
INNER JOIN uf on uf.nome = cidade_aux.uf 
INNER JOIN regiao_uf on regiao_uf.nome = cidade_aux.regiao
ORDER BY cidade_aux.uf, cidade_aux.nome;

*/