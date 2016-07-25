<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:lib="http://library.by/catalog" 
  extension-element-prefixes="lib">

  <xsl:output method="html" indent="yes"/>
  
  <xsl:param name="Date" select="''"/>

  <xsl:template match="/">
    <html>
      <head>
        <title>
          Current funds by generes - <xsl:value-of select="$Date"/>
        </title>
      </head>
      <body>
        <h2>
          Current funds by generes - <xsl:value-of select="$Date"/>
        </h2>
        <xsl:for-each select="//lib:book[not(lib:genre/text()=preceding::lib:genre/text())]/lib:genre/text()">
          <h3>
            <xsl:value-of select="."/>
          </h3>
          <xsl:call-template name="genreTable" >
          <xsl:with-param name ="genreName">
            <xsl:value-of select="."/>
          </xsl:with-param>
        </xsl:call-template>
        </xsl:for-each>
        Total count: <xsl:value-of select="count(//lib:book)"/>
      </body>
    </html>
  </xsl:template>

  <xsl:template name="genreTable" >
    <xsl:param name="genreName">Empty</xsl:param>

    <table border="1">
      <tr bgcolor="#d4b97d">
        <th style="text-align:left">Author</th>
        <th style="text-align:left">Title</th>
        <th style="text-align:left">Publish date</th>
        <th style="text-align:left">Registration date</th>
      </tr>
      <xsl:for-each select="//lib:book[lib:genre = $genreName]">
        <tr>
          <td>
            <xsl:value-of select="lib:author" />
          </td>
          <td>
            <xsl:value-of select="lib:title" />
          </td>
          <td>
            <xsl:value-of select="lib:publish_date" />
          </td>
          <td>
            <xsl:value-of select="lib:registration_date" />
          </td>
        </tr>
      </xsl:for-each>
    </table>
    <h4>
      Count: <xsl:value-of select="count(//lib:book[lib:genre = $genreName])"/>
      ----------------------------------------------
    </h4>
    
  </xsl:template>

</xsl:stylesheet>
