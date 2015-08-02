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
        [TARGET] [COMMAND] [COLOR] [SPEED] [REPETITIONS]
    
    TARGET
        all | front | back | led1 | led2 | led3 | led4 | led5 | led6
        default : all
    
    COMMAND
        color | blink | wave | pattern
        default : color
    
    COLOR
        color name (red | green | blue | ...) or hexadecimal code (with or without hash character)
        
    SPEED
        Integer. Only for color and blink commands.
        
    REPETITIONS
        Integer. Only for wave and pattern commands.

### Exemples

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
