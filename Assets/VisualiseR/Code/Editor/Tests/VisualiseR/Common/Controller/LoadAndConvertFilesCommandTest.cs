using System.IO;
using NUnit.Framework;
using strange.extensions.injector.api;
using VisualiseR.Main;

namespace VisualiseR.Common
{
    public class LoadAndConvertFilesCommandTest
    {
        //TODO mocken von Files mal probieren
        //http://stackoverflow.com/questions/1087351/how-do-you-mock-out-the-file-system-in-c-sharp-for-unit-testing
        //TODO überprüfen von Signalen, ob und mit welchem Parameter sie versendet wurden.
        // vielleicht in den .tests von strangeIoC gucken...
        // https://www.nunit.org/index.php?p=exceptionAsserts&r=2.5

        [Test]
        public void testWrongPath()
        {
            //given
            var loadDiskCommand = new LoadFilesCommand {uri = "asdasd"};

            //when
            Assert.That(() => loadDiskCommand.Execute(),

                // then
                Throws.TypeOf<FileNotFoundException>());
        }
    }
}