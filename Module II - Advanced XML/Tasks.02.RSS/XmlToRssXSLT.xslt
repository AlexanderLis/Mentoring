<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet 
  version="1.0" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:lib="http://library.by/catalog" 
  extension-element-prefixes="lib">
  
    <xsl:output method="xml" indent="yes"/>

  <xsl:template match="/lib:catalog">
    <xsl:element name="rss">
      <xsl:attribute name="version">2.0</xsl:attribute>
      <xsl:element name="channel">
        <title>Library catalog</title>
        <link>http://my.safaribooksonline.com/</link>
        <description>Books channel for you</description>
        <language>en-us</language>
        <pubDate>Tue, 10 Jun 2003 04:00:00 GMT</pubDate>
        <lastBuildDate>Tue, 10 Jun 2016 09:41:01 GMT</lastBuildDate>
        <webMaster>library@safari.com</webMaster>
        <xsl:apply-templates />
      </xsl:element>
    </xsl:element>
  </xsl:template>

  <xsl:template match="lib:book/text()"/>

  <xsl:template match="lib:book">
    <xsl:element name="item">
      <xsl:apply-templates/>
    </xsl:element>
  </xsl:template>
  
  <xsl:template match="lib:title">
    <xsl:text disable-output-escaping="yes">&#010;</xsl:text>
    <xsl:element name="title">
      <xsl:value-of select="."/>
    </xsl:element>
  </xsl:template>

  <xsl:template match="lib:description">
    <xsl:text disable-output-escaping="yes">&#010;</xsl:text>
    <xsl:element name="description">
      <xsl:value-of select="."/>
    </xsl:element>
  </xsl:template>
  
  <xsl:template match="lib:registration_date">
    <xsl:text disable-output-escaping="yes">&#010;</xsl:text>
    <xsl:element name="pubDate">
      <xsl:value-of select="."/>
    </xsl:element>
    <xsl:text disable-output-escaping="yes">&#010;</xsl:text>
  </xsl:template>

  <xsl:template match="*"/>

  <xsl:template match="/lib:catalog/lib:book[lib:genre = 'Computer' and lib:isbn]/lib:isbn">
    <xsl:element name="link">
      http://my.safaribooksonline.com/<xsl:value-of select="text()" />/
    </xsl:element>
  </xsl:template>
  
  
</xsl:stylesheet>
