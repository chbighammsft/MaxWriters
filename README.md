# MaxWriters -- Documentation writers for REST endpoints

Modern Assistence Experience (MAX) has created this writer so that you can generate 
documentation for a REST endpoint. It uses the OData spec and the Annotations vocabulary
to create basic reference documentation.

<a name="SettingUp"></a>
## Setting up
The documentation writers depend on the annotation branch of the Vipr tool to create
REST documentation. To run the writer, you need:

1. The [MaxWriters project](https://github.com/chbighammsft/MaxWriters). 
   Download or clone the solution from GitHub.
1. The [Annotations branch of the Vipr project](https://github.com/Microsoft/Vipr/tree/Annotations). 
   Download or clone the solution from GitHub.

<a name="BuildingTheProject"></a>
## Building the projects.
You need to build both the Vipr and the MaxWriters solutions to run the documentation writers. 
Once you've built the solutions, open these directories:

* <-RepoLocation->\Vipr\src\Core\Vipr\bin\Debug
* <-RepoLocation->\MaxWriters\src\MAX.Writers.REST\bin\Debug

Copy MAX.Writers.REST.dll from the MaxWriters solution to the Vipr solution.

<a name="RunningTheWriter"></a>
## Running the documentation writer
Here's what you need to do to run the documentation writer:

1. Open a command prompt in <-RepoLocation->\Vipr\src\Core\Vipr\bin\Debug
2. Run the following command line. 
   
    vipr <-Location of the metadata file->
         --writer=MAX.Writer.REST
		 -- outputPath=<-Location to write doc file->
