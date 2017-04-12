// Auto-generated code below aims at helping you parse
// the standard input according to the problem statement.
program Answer;
{$H+}
uses sysutils, classes, math;

// Helper to read a line and split tokens
procedure ParseIn(Inputs: TStrings) ;
var Line : string;
begin
    readln(Line);
    Inputs.Clear;
    Inputs.Delimiter := ' ';
    Inputs.DelimitedText := Line;
end;

var
    factoryCount : Int32; // the number of factories
    linkCount : Int32; // the number of links between factories
    factory1 : Int32;
    factory2 : Int32;
    distance : Int32;
    entityCount : Int32; // the number of entities (e.g. factories and troops)
    entityId : Int32;
    entityType : String;
    arg1 : Int32;
    arg2 : Int32;
    arg3 : Int32;
    arg4 : Int32;
    arg5 : Int32;
    i : Int32;
    Inputs: TStringList;
begin
    Inputs := TStringList.Create;
    ParseIn(Inputs);
    factoryCount := StrToInt(Inputs[0]);
    ParseIn(Inputs);
    linkCount := StrToInt(Inputs[0]);
    for i := 0 to linkCount-1 do
    begin
        ParseIn(Inputs);
        factory1 := StrToInt(Inputs[0]);
        factory2 := StrToInt(Inputs[1]);
        distance := StrToInt(Inputs[2]);
    end;

    // game loop
    while true do
    begin
        ParseIn(Inputs);
        entityCount := StrToInt(Inputs[0]);
        for i := 0 to entityCount-1 do
        begin
            ParseIn(Inputs);
            entityId := StrToInt(Inputs[0]);
            entityType := Inputs[1];
            arg1 := StrToInt(Inputs[2]);
            arg2 := StrToInt(Inputs[3]);
            arg3 := StrToInt(Inputs[4]);
            arg4 := StrToInt(Inputs[5]);
            arg5 := StrToInt(Inputs[6]);
        end;

        // Write an action using writeln()
        // To debug: writeln(StdErr, 'Debug messages...');


        // Any valid action, such as "WAIT" or "MOVE source destination cyborgs"
        writeln('WAIT');
        //flush(StdErr);
        flush(output); // DO NOT REMOVE
    end;
end.
