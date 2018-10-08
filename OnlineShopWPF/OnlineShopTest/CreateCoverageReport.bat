IF not exist "bin\debugCoverageReport" mkdir "bin\Debug\CoverageReport"
%CVDEV_INTERNAL%\win32\NetCoverage\OpenCover\OpenCover.Console.exe -register:user -target:"bin\Debug\OnlineShopTest.exe" -output:"bin\Debug\CoverageReport\CoverageResult.xml" -filter:"+[OnlineShop]*" -log:Error
%CVDEV_INTERNAL%\win32\NetCoverage\ReportGenerator\bin\ReportGenerator.exe -reports:"bin\Debug\CoverageReport\CoverageResult.xml" -targetdir:"bin\Debug\CoverageReport" -reporttypes:Html -verbosity:Error
start "" "bin\Debug\CoverageReport\index.htm"