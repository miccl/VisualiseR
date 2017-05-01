using System.IO;
using NUnit.Framework;
using strange.extensions.injector.api;

namespace VisualiseR.Common
{
    public class LoadAndConvertFilesCommandTest
    {
        //TODO mocken von Files mal probieren
        //http://stackoverflow.com/questions/1087351/how-do-you-mock-out-the-file-system-in-c-sharp-for-unit-testing
        //TODO überprüfen von Signalen, ob und mit welchem Parameter sie versendet wurden.
        // vielleicht in den .tests von strangeIoC gucken...
        // https://www.nunit.org/index.php?p=exceptionAsserts&r=2.5

        IInjectionBinder injectionBinder;

        [Test]
        public void testWrongPath()
        {
            //given
            var loadDiskCommand = new LoadDiskDataCommand {uri = "asdasd"};

            //when
            Assert.That(() => loadDiskCommand.Execute(),

                // then
                Throws.TypeOf<FileNotFoundException>());
        }

//        [Test]
//        public void testFilePath()
//        {
//            // given
//            var loadDiskCommand = new LoadDiskDataCommand {_directoryPath = "D:/VisualiseR_Test/imgres.jpg"};
//
//            //
//
//        }
//
//        [Test]
//        public void testEmptyFolderPath()
//        {
//            // given
//            var loadDiskCommand = new LoadDiskDataCommand {_directoryPath = "D:/VisualiseR_Test/EmptyDirectory"};
//
//            //when
//            loadDiskCommand.Execute();
//
//            //then
//        }
//
//        [Test]
//        public void testFullFolderPath()
//        {
//            // given
//            var loadDiskCommand = new LoadDiskDataCommand {_directoryPath = "D:/VisualiseR_Test/FullDirectory"};
//
//            //when
//            loadDiskCommand.Execute();
//
//            //then
//        }
    }
}