# InterviewSCRMLidl

INFO:
-> If project fails when compiling:
	A- Check if there's a missmatch with your local version an server version (right click solution project and click Undo...).
	B- If A doesn't work, edit UserStories.AcceptanceTests.csproj Delete <ItemGroup>'s if the contain (...)feature.cs:
		ie:
		<ItemGroup>
			<Compile Update="Features\ForumService\ForumService.feature.cs">
			  <DesignTime>True</DesignTime>
			  <AutoGen>True</AutoGen>
			  <DependentUpon>ForumService.feature</DependentUpon>
			</Compile>
		</ItemGroup>
		This happens when specflow feature files are not well auto-generated.

-> If you see the steps in purple (step not found)
	1 - Close visual studio.
	2 - Open %TEMP% folder, delete specflow files (they'll regenerate again).
	3 - Open visual studio.
	4 - Build solution.
	
-> The tests will appear in test explorer. Open Test > Window > Test Explorer

-> Right click in the tests, Debug or Run.