<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:lib="http://cob.wk/specification" 
                xmlns:eubas="https://schema.wcsc.com/cob/specification/eubas"
                xmlns:bas96="https://schema.wcsc.com/cob/specification/bas96"
  extension-element-prefixes="lib eubas bas96">

  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/">
    <html>
      <body>
        <h2>Specifications</h2>
        <table border="1">
          <tr bgcolor="#d4b97d">
            <th style="text-align:left">Form ID</th>
            <th style="text-align:left">Caption</th>
            <th style="text-align:left">Category</th>
            <th style="text-align:left">Description</th>
            <th style="text-align:left">Eubas name</th>
            <th style="text-align:left">Bas96 name</th>
            <th style="text-align:left">Tax year</th>
            <th style="text-align:left">Hint</th>
          </tr>
          <xsl:for-each select="lib:specification-list/lib:specification">
            <tr>
              <td>
                <xsl:value-of select="lib:FormID" />
              </td>
              <td>
                <xsl:value-of select="lib:Caption" />
              </td>
              <td>
                <xsl:value-of select="lib:Category" />
              </td>
              <td>
                <xsl:value-of select="lib:Description" />
              </td>
              <td>
                <xsl:value-of select="lib:Name/eubas:Name" />
              </td>
              <td>
                <xsl:value-of select="lib:Name/bas96:Name" />
              </td>
              <td>
                <xsl:value-of select="lib:TaxYear" />
              </td>
              <td>
                <xsl:value-of select="lib:Hint" />
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>
