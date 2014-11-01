 
  select EBA.No_Credito,
                   EBA.Grupo,
                  --  TEC.Dx_Nombre_General,
                    EBA.Dx_Modelo_Producto,
                     EBA.CapacidadSistema,
                    EBA.Unidad,
                     EBA.No_Unidades,
                    EBA.Dx_Marca,
                     EBA.Dx_Modelo_Producto,
                    EBA.Dx_Color,
                     EBA.Dx_Antiguedad,
                     EBA.Id_Pre_Folio,
                     EBA.Id_Folio,
                    EBA.Dt_Fecha_Recepcion,
                    case when EBA.Fg_Tipo_Centro_Disp = 'M' then 
					CCD.Dx_Nombre_Comercial else CCDS.Dx_Nombre_Comercial end,
                  case when EBA.Fg_Tipo_Centro_Disp = 'M' then CCD.Dx_Razon_Social else CCDS.Dx_Razon_Social end,
                   case when EBA.Fg_Tipo_Centro_Disp = 'M' then z.Dx_Nombre_Zona else zo.Dx_Nombre_Zona end,
                    case when 
					EBA.Fg_Tipo_Centro_Disp = 'M' then r.Dx_Nombre_Region else re.Dx_Nombre_Region end,
                   FT.idConsecutivoFoto,
                 FT.idTipoFoto,
                    FT.IdCreditoSustitucion
 from  --CRE_CREDITO_EQUIPOS_BAJA EBA
  K_CREDITO_SUSTITUCION EBA 
   join cat_tecnologia t on EBA.cve_tecnologia = t.cve_tecnologia
join CAT_TIPO_PRODUCTO p on EBA.Dx_Tipo_Producto = p.Ft_Tipo_Producto  
 LEFT join CAT_CAPACIDAD_SUSTITUCION capSus on EBA.Cve_Capacidad_Sust= capSus.Cve_Capacidad_Sust 
                left join  CRE_FOTOS  FT on EBA.No_Credito = FT.No_Credito
                left join  CAT_CENTRO_DISP CCD on EBA.Id_Centro_Disp = CCD.Id_Centro_Disp
                 left join  CAT_CENTRO_DISP_SUCURSAL CCDS on EBA.Id_Centro_Disp = CCDS.Id_Centro_Disp
                left join  CAT_REGION r on CCD.Cve_Region = r.Cve_Region
                left join  CAT_ZONA z on CCD.Cve_Zona = z.Cve_Zona
                left join   CAT_REGION re on CCDS.Cve_Region = re.Cve_Region
                
				
				left join  CAT_ZONA zo on CCDS.Cve_Zona = zo.Cve_Zona
                where EBA.No_Credito = 'PAEEEMDA01A18073' 
				and FT.idTipoFoto = 2
				and ft.IdCreditoSustitucion = EBA.Id_Credito_Sustitucion
               
			 --  select * from cre_credito

			select * from CAT_TIPO_PRODUCTO 
				select * from K_CREDITO_SUSTITUCION 
			
			 where No_Credito = 'PAEEEMDA01A18073' 

			 --select * from US_USUARIO where Id_Departamento = 23

			 --update CRE_Credito set Cve_Estatus_Credito = 1  where No_Credito = 'PAEEEMDA01A18073' 