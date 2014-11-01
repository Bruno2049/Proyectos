<?xml version="1.0"?>
<!--
Version 11.1.20111.2158
Copyright (c) 2001-2012 Infragistics, Inc. All Rights Reserved.
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output omit-xml-declaration="yes" method="html" indent="no"/>

<xsl:key name="columnIndex" match="Column" use="@index"/>
<xsl:key name="cellIndex" match="C" use="position()"/>

<xsl:variable name="gridName" select="//UltraWebGrid/@id"/>
<xsl:variable name="useFixedHeaders" select="//UltraWebGrid/UltraGridLayout/@UseFixedHeaders"/>
<xsl:variable name="tableLayout" select="//UltraWebGrid/UltraGridLayout/@TableLayout"/>
<xsl:variable name="isXhtml" select="//UltraWebGrid/UltraGridLayout/@IsXhtml"/>
<xsl:variable name="fixedScrollLeft" select="//UltraWebGrid/UltraGridLayout/@fixedScrollLeft"/>

<xsl:template match="/">
	<xsl:apply-templates select="Rs" />
</xsl:template>

<xsl:template match="Rs">
	<tbody>
		<xsl:apply-templates select="R">
			<xsl:with-param name="bandNo" select="@bandNo"/>
			<xsl:with-param name="parentRowLevel" select="@parentRowLevel"/>
		</xsl:apply-templates>
		<xsl:apply-templates select="Group">
			<xsl:with-param name="bandNo" select="@bandNo"/>
			<xsl:with-param name="parentRowLevel" select="@parentRowLevel"/>
		</xsl:apply-templates>
	</tbody>
</xsl:template>

<xsl:template match="R">
	<xsl:param name="bandNo"/>
	<xsl:param name="parentRowLevel"/>

	<xsl:variable name="band" select="//UltraWebGrid/Bands/Band[number($bandNo)]"/>
	<xsl:variable name="fac" select="number($band/@fac)"/>
	<xsl:variable name="rs" select="number($band/@rs)"/>
	<xsl:variable name="rowIndex" select="@i"/>
	<xsl:variable name="rHeight">
		<xsl:choose>
			<xsl:when test="@height">
				<xsl:value-of select="@height" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$band/@rowHeight" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>

	<tr id="{$gridName}_r_{$parentRowLevel}{$rowIndex}" level="{$parentRowLevel}{$rowIndex}">
		<xsl:if test="$band/@optSelectRow">
			<xsl:attribute name="class">
				<xsl:choose>
					<xsl:when test="$rowIndex mod 2 = 1">
						<xsl:value-of select="$band/@altClass" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:value-of select="$band/@itemClass" />
					</xsl:otherwise>
				</xsl:choose>
			</xsl:attribute>
		</xsl:if>
		<xsl:attribute name="style">
			<xsl:if test="@hidden">display:none;</xsl:if>
			<xsl:value-of select="concat('height:',$rHeight,';')" />
		</xsl:attribute>
		<xsl:if test="$fac>1 or $rs=2 and $fac=1">
			<th class="{$band/@expAreaClass}">
				<xsl:attribute name="height">
					 <xsl:value-of select="$rHeight"/>
				</xsl:attribute>
				<xsl:if test="$band/@ind = 0">
					<xsl:attribute name="style">display:none;</xsl:attribute>
				</xsl:if>
				<xsl:element name="img">
					<xsl:attribute name="border">0</xsl:attribute>
					<xsl:choose>
						<xsl:when test="@showExpand">
							<xsl:attribute name="src">
								<xsl:value-of select="$band/@expandImage" />
							</xsl:attribute>
							  <xsl:attribute name="alt">
								<xsl:value-of select="$band/@xAlt"/>
							  </xsl:attribute>
							  <xsl:attribute name="igAltC">
								<xsl:value-of select="$band/@cAlt"/>
							  </xsl:attribute>
							<xsl:attribute name="imgType">expand</xsl:attribute>
						</xsl:when>
						<xsl:otherwise>
							<xsl:attribute name="src">
								<xsl:value-of select="$band/@blankImage" />
							</xsl:attribute>
							<xsl:attribute name="imgType">blank</xsl:attribute>
							<xsl:attribute name="style">visibility:hidden;</xsl:attribute>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:element>
			</th>
		</xsl:if>
		<xsl:if test="$fac>0 and $rs!=2">
			<th id="{$gridName}_l_{$parentRowLevel}{$rowIndex}" class="{$band/@rowLabelClass}">
				<xsl:attribute name="style">text-align:center;vertical-align:middle;<xsl:if test="@rowSelectStyle"><xsl:value-of select="@rowSelectStyle" disable-output-escaping="yes" />;</xsl:if>
					<xsl:if test="$band/@rowHeight">height:<xsl:value-of select="$band/@rowHeight" disable-output-escaping="yes" />;</xsl:if>
				</xsl:attribute>
				<xsl:choose>
					<xsl:when test="@rowNumber">
						<xsl:value-of select="@rowNumber" disable-output-escaping="yes" />
					</xsl:when>
					<xsl:otherwise>
						<xsl:element name="img">
							<xsl:attribute name="src">
								<xsl:value-of select="$band/@rowLabelBlankImage" />
							</xsl:attribute>
              <xsl:attribute name="alt"></xsl:attribute>
							<xsl:attribute name="border">0</xsl:attribute>
							<xsl:attribute name="imgType">blank</xsl:attribute>
						</xsl:element>
					</xsl:otherwise>
				</xsl:choose>
			</th>
		</xsl:if>
		<xsl:apply-templates select="Cs">
			<xsl:with-param name="band" select="$band"/>
			<xsl:with-param name="rowIndex" select="$rowIndex" />
			<xsl:with-param name="row" select="." />
			<xsl:with-param name="rowHeight" select="$rHeight" />
			<xsl:with-param name="parentRowLevel" select="$parentRowLevel" />
		</xsl:apply-templates>
	</tr>
</xsl:template>

<xsl:template match="Cs">
	<xsl:param name="band"/>
	<xsl:param name="rowIndex" />
	<xsl:param name="row" />
	<xsl:param name="rowHeight" />
	<xsl:param name="parentRowLevel"/>

	<xsl:for-each select="$band/Columns/Column">
		<xsl:if test="not(@grouped) and not(@serverOnly) and not(@nonfixed)">
			<xsl:variable name="columnIndex">
				<xsl:value-of select="@cellIndex"/>
			</xsl:variable>
			<xsl:variable name="cell" select="$row/Cs/C[number($columnIndex)]"/>
			<xsl:choose>
				<xsl:when test="$rowIndex mod 2 = 1">
					<xsl:call-template name="cellTemplate">
						<xsl:with-param name="cell" select="$cell" />
						<xsl:with-param name="rowIndex" select="$rowIndex" />
						<xsl:with-param name="className">
							<xsl:if test="not($band/@optSelectRow)">
								<xsl:value-of select="$band/@altClass" />
							</xsl:if>
						</xsl:with-param>
						<xsl:with-param name="rowHeight" select="$rowHeight" />
						<xsl:with-param name="parentRowLevel" select="$parentRowLevel" />
						<xsl:with-param name="rowStyle" select="$row/@style" />
						<xsl:with-param name="rowClass" select="$row/@class" />
					</xsl:call-template>
				</xsl:when>
				<xsl:otherwise>
					<xsl:call-template name="cellTemplate">
						<xsl:with-param name="cell" select="$cell" />
						<xsl:with-param name="rowIndex" select="$rowIndex" />
						<xsl:with-param name="className">
							<xsl:if test="not($band/@optSelectRow)">
								<xsl:value-of select="$band/@itemClass" />
							</xsl:if>
						</xsl:with-param>
						<xsl:with-param name="rowHeight" select="$rowHeight" />
						<xsl:with-param name="parentRowLevel" select="$parentRowLevel" />
						<xsl:with-param name="rowStyle" select="$row/@style" />
						<xsl:with-param name="rowClass" select="$row/@class" />
					</xsl:call-template>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:if>
	</xsl:for-each>

	<xsl:if test="$useFixedHeaders">
		<td colspan="{$band/@nfspan}">
			<xsl:choose>
				<xsl:when test="$band/@optSelectRow">
					<xsl:attribute name="class">
						<xsl:value-of select="concat($gridName,'-no')" />
					</xsl:attribute>
					<xsl:if test="$row/@dtdh">
						<xsl:attribute name="height">
							<xsl:value-of select="$row/@dtdh" />
						</xsl:attribute>
					</xsl:if>
				</xsl:when>
				<xsl:otherwise>
					<xsl:attribute name="style">
						<xsl:choose>
							<xsl:when test="$row/@dtdh">
								<xsl:value-of select="concat('vertical-align:top;height:',$row/@dtdh,';')" />
							</xsl:when>
							<xsl:otherwise>vertical-align:top;width:100%;</xsl:otherwise>
						</xsl:choose>
					</xsl:attribute>
				</xsl:otherwise>
			</xsl:choose>
			<div id="{$gridName}_drs">
				<xsl:attribute name="style">
					<xsl:choose>
						<xsl:when test="$row/@dtdh">overflow:hidden;<xsl:value-of select="$row/@ht"/><xsl:if test="$isXhtml">position:relative;</xsl:if></xsl:when>
						<xsl:otherwise>overflow:hidden;width:100%;height:100%;<xsl:if test="$isXhtml">position:relative;</xsl:if></xsl:otherwise>
					</xsl:choose>
				</xsl:attribute>
					<table width="1"  border="0" cellspacing="0" cellpadding="0" style="position:relative;table-layout:fixed;height:100%;{$fixedScrollLeft}">
						<colgroup>
						<xsl:for-each select="$band/Columns/Column">
							<xsl:if test="not(@grouped) and not(@serverOnly) and not(@hidden) and @nonfixed">
								<col width="{@width}"/>
							</xsl:if>
						</xsl:for-each>
						<xsl:for-each select="$band/Columns/Column">
							<xsl:if test="not(@grouped) and not(@serverOnly) and @hidden and @nonfixed">
								<col width="1px" style="display:none;"/>
							</xsl:if>
						</xsl:for-each>
					</colgroup>
						<tr id="{$gridName}_nfr_{$parentRowLevel}{$rowIndex}">
						<xsl:attribute name="style">
							<xsl:choose>
								<xsl:when test="$row/@height">
									<xsl:value-of select="concat('height:',$row/@height,';')" />
								</xsl:when>
								<xsl:otherwise>
									<xsl:value-of select="concat('height:',$rowHeight,';')" />
								</xsl:otherwise>
							</xsl:choose>
						</xsl:attribute>
						<xsl:for-each select="$band/Columns/Column">
							<xsl:variable name="columnIndex">
								<xsl:value-of select="@cellIndex"/>
							</xsl:variable>
							<xsl:variable name="cell" select="$row/Cs/C[number($columnIndex)]"/>
							<xsl:if test="not(@grouped) and not(@serverOnly) and @nonfixed">
								<xsl:choose>
									<xsl:when test="$rowIndex mod 2 = 1">
										<xsl:call-template name="cellTemplate">
											<xsl:with-param name="cell" select="$cell" />
											<xsl:with-param name="rowIndex" select="$rowIndex" />
											<xsl:with-param name="className">
												<xsl:if test="not($band/@optSelectRow)">
													<xsl:value-of select="$band/@altClass" />
												</xsl:if>
											</xsl:with-param>
											<xsl:with-param name="rowHeight" select="$rowHeight" />
											<xsl:with-param name="parentRowLevel" select="$parentRowLevel" />
											<xsl:with-param name="rowStyle" select="$row/@style" />
											<xsl:with-param name="rowClass" select="$row/@class" />
										</xsl:call-template>
									</xsl:when>
									<xsl:otherwise>
										<xsl:call-template name="cellTemplate">
											<xsl:with-param name="cell" select="$cell" />
											<xsl:with-param name="rowIndex" select="$rowIndex" />
											<xsl:with-param name="className">
												<xsl:if test="not($band/@optSelectRow)">
													<xsl:value-of select="$band/@itemClass" />
												</xsl:if>
											</xsl:with-param>
											<xsl:with-param name="rowHeight" select="$rowHeight" />
											<xsl:with-param name="parentRowLevel" select="$parentRowLevel" />
											<xsl:with-param name="rowStyle" select="$row/@style" />
											<xsl:with-param name="rowClass" select="$row/@class" />
										</xsl:call-template>
									</xsl:otherwise>
								</xsl:choose>
							</xsl:if>
						</xsl:for-each>
					</tr>
				</table>
			</div>
		</td>
	</xsl:if>
</xsl:template>

<xsl:template name="cellTemplate">
	<xsl:param name="cell" />
	<xsl:param name="rowIndex" />
	<xsl:param name="className" />
	<xsl:param name="rowHeight" />
	<xsl:param name="parentRowLevel"/>
	<xsl:param name="rowStyle"/>
	<xsl:param name="rowClass"/>
	
	<xsl:if test="not($cell/@merged)">
	
		<xsl:variable name="cellIndex">
			<xsl:value-of select="position()-1"/>
		</xsl:variable>

		<td id="{$gridName}_rc_{$parentRowLevel}{$rowIndex}_{$cellIndex}">
			<xsl:if test="$cell/@rowSpan">
				<xsl:attribute name="rowSpan">
					<xsl:value-of select="$cell/@rowSpan" />
				</xsl:attribute>
			</xsl:if>
			<xsl:if test="$cell/@colSpan">
				<xsl:attribute name="colSpan">
					<xsl:value-of select="$cell/@colSpan" />
				</xsl:attribute>
			</xsl:if>
			<xsl:if test="$cell/@title">
				<xsl:attribute name="title">
					<xsl:value-of select="$cell/@title" />
				</xsl:attribute>
			</xsl:if>
			<xsl:if test="string-length($className)>0 or ./@class or $rowClass or $cell/@class">
				<xsl:attribute name="class">
					<xsl:value-of select="concat($className,' ',./@class,' ',$rowClass,' ',$cell/@class)" />
				</xsl:attribute>
			</xsl:if>
			<xsl:if test="./@style or $rowStyle or $cell/@style or @hidden">
				<xsl:attribute name="style">
					<xsl:value-of select="concat(./@style,$rowStyle,$cell/@style)" />
					<xsl:if test="@hidden">display:none;</xsl:if>
				</xsl:attribute>
			</xsl:if>
			<xsl:if test="$cell/@allowedit">
				<xsl:attribute name="allowedit">
					<xsl:value-of select="$cell/@allowedit" />
				</xsl:attribute>
			</xsl:if>
			<xsl:if test="$cell/@uV">
				<xsl:attribute name="uV">
					<xsl:value-of select="$cell/@uV" />
				</xsl:attribute>
			</xsl:if>
			<xsl:if test="$cell/@iCT">
				<xsl:attribute name="iCT">
					<xsl:value-of select="$cell/@iCT" />
				</xsl:attribute>
			</xsl:if>
			<xsl:if test="$cell/@iDV">
				<xsl:attribute name="iDV">
					<xsl:value-of select="$cell/@iDV" />
				</xsl:attribute>
			</xsl:if>
			<xsl:if test="$cell/@iTM">
				<xsl:attribute name="iTM">
					<xsl:value-of select="$cell/@iTM" />
				</xsl:attribute>
			</xsl:if>
			<xsl:choose>
				<xsl:when test="$cell/@br"><xsl:if test="$cell/@doe"><xsl:attribute name="name">_igdoe</xsl:attribute></xsl:if><xsl:value-of select="$cell" disable-output-escaping="yes" /></xsl:when>
				<xsl:otherwise><nobr><xsl:if test="$cell/@doe"><xsl:attribute name="name">_igdoe</xsl:attribute></xsl:if><xsl:value-of select="$cell" disable-output-escaping="yes" /></nobr></xsl:otherwise>
			</xsl:choose>
		</td>
	</xsl:if>
</xsl:template>

<xsl:template match="Group">
	<xsl:param name="bandNo"/>
	<xsl:param name="parentRowLevel"/>
	
	<xsl:variable name="band" select="//UltraWebGrid/Bands/Band[number($bandNo)]"/>
	<xsl:variable name="rowIndex" select="@i"/>
	<xsl:variable name="rHeight">
		<xsl:choose>
			<xsl:when test="@height">
				<xsl:value-of select="@height" />
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$band/@rowHeight" />
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	
	<tr id="{$gridName}_gr_{$parentRowLevel}{$rowIndex}" groupRow="{@groupRow}" level="{$parentRowLevel}{$rowIndex}">
		<xsl:attribute name="style">height:<xsl:value-of select="$rHeight" />;<xsl:if test="$isXhtml and $useFixedHeaders">position:relative;</xsl:if></xsl:attribute>
		<td>
			<table border='0' cellpadding='0' cellspacing='0' bgcolor='{@bgcolor}' bandNo='{@bandNo}'>
				<xsl:if test="$tableLayout">
					<xsl:attribute name="style">table-layout:fixed;</xsl:attribute>
				</xsl:if>
				<xsl:attribute name="width"><xsl:value-of select="../@grpWidth" disable-output-escaping="yes" /></xsl:attribute>
				<tr id="{$gridName}_sgr_{$parentRowLevel}{$rowIndex}" level="{$parentRowLevel}{$rowIndex}" groupRow="{@groupRow}">
					<xsl:attribute name="style">height:<xsl:value-of select="$rHeight" />;</xsl:attribute>
					<td id="{$gridName}_grc_{$parentRowLevel}{$rowIndex}" groupRow="{@groupRow}" style='{@style}' class='{$band/@grpClass}'>
						<xsl:attribute name="cellValue">
							<xsl:value-of select="@cellValue" disable-output-escaping="yes" />
						</xsl:attribute>
						<xsl:element name="img">
							<xsl:attribute name="border">0</xsl:attribute>
							<xsl:attribute name="src">
								<xsl:value-of select="$band/@expandImage" />
							</xsl:attribute>
              <xsl:attribute name="alt">
                <xsl:value-of select="$band/@xAlt"/>
              </xsl:attribute>
              <xsl:attribute name="igAltC">
                <xsl:value-of select="$band/@cAlt"/>
              </xsl:attribute>
							<xsl:attribute name="imgType">expand</xsl:attribute>
						</xsl:element>
						<xsl:text>&#xa0;&#xa0;</xsl:text>
						<xsl:value-of select="@content" disable-output-escaping="yes" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
</xsl:template>

</xsl:stylesheet>
