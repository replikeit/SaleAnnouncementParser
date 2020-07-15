# Sale Announcements Parser
<h3>Description</h3> 
Parser for tracking sale announcements on two games sites: g2g.com, funpay.ru<br/>
The program is written on C# with TCP for transporting data and AngleSharp, CefSharp for parsing<br/><br/>
<h3>Installation</h3> 
<ul>
  <li>
    <p>Put "Parser & Server" on the server, and then copy DLL's to the directory.</p>
  </li>
  <li>
    <p>Put "Reader" on the clients hosts.</p>
  </li>
</ul>
<br/><br/>
<h3>Manual</h3>
<ul>
  <li>
    <p>First of all, set the settings for the “settings.cfg” file in the “Reader” and “Parser & Server” directories.</p>
    <img width="200px" src="https://sun9-45.userapi.com/c858416/v858416325/2189de/Ii0-G64NcxA.jpg"/>
    <p>Set current server ip and port.</p>
  </li>
</ul>
<h4>Guide for a Sever</h4>
<ul>
  <li>
    <p>On the server, run “Server.exe” and “Parser.exe”, then turn on the server.</p>
    <img width="250px" src="https://sun9-34.userapi.com/c858416/v858416325/2189fc/TRMQZ5yDsTk.jpg"/>
  </li>
  <li>
    <p>After launch "Parser.exe",parsing sites starts automatically.</p>
    <img width="450px" src="https://sun9-28.userapi.com/c858416/v858416325/218a15/B2pCCWZeVPY.jpg"/>
  </li>
  <li>
    <p>After each cycle, the parser saves data about sites in the file "CurrentData.bin", which is then transmitted to the client.</p>
  </li>
</ul>
<h4>Guide for a Client</h4>
