# LuxaforCli

A simple command line application to control Luxafor devices.

This app is based on the LuxaforSharp .Net library by Duncan-Idaho:  
https://github.com/Duncan-Idaho/LuxaforSharp

## About Luxafor

Luxafor is an LED indicator that connects to your computer through a USB port 
or via Bluetooth, and shows your availability or notifies you about important 
information, like incoming emails or calendar reminders.

Its Hardware Api is open, allowing developers to control the device through
their own applications.

You can go to http://luxafor.com/ to get more information about it.

## Usage

    LuxaforCli.exe COMMAND_GROUP...

        COMMAND_GROUP
            [color] [TARGET]    COLOR [SPEED]
            blink   [TARGET]    COLOR [SPEED] [REPETITIONS]
            wave    [WAVETYPE]  COLOR [SPEED] [REPETITIONS]
            pattern [PATTERNID]               [REPETITIONS]

        TARGET
            all | front | back | led1 | led2 | led3 | led4 | led5 | led6
            default : all

        COLOR
            color name (red | green | blue | ...) | hexadecimal code | ""off""

        SPEED
            0-255

        REPETITIONS
            0-255

        WAVETYPE
            Short | Long  | OverlappingShort | OverlappingLong

        PATTERNID
            Luxafor | Police | Random1 | Random2 | Random3 | Random4 | Random5 | RainbowWave

    Examples:

        LuxaforCli.exe  red  

        LuxaforCli.exe  front dd4f00  

        LuxaforCli.exe  red   led1 green   led4 green

        LuxaforCli.exe  led1 green   led2 yellow   led3 red   back cyan   blink led5 blue 20 5

    LED layout:

            +-------,
            |6 3    |
      back  |5 2    |  front
            |4 1    |
            |   +---'
            |   |
            |   |
            +---+

### Examples

```LuxaforCli.exe``` ```red```  
Turns all leds red.

```LuxaforCli.exe``` ```front dd4f00```  
Turns all front leds orange-ish.

```LuxaforCli.exe``` ```red``` ```led1 green``` ```led6 blink blue```  
Turns all leds red, front-bottom led green and back-top blue.

## Development / build requirements

LuxaforCli development is based on .Net 4.5.

It requires LuxaforSharp , which is available as a NuGet (See: http://www.nuget.org/packages/HidLibrary/).

LuxaforSharp itself also requires HidLibrary (https://github.com/mikeobrien/HidLibrary).
