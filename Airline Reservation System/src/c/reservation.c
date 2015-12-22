#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "passenger.h"
#include "scheduledFlight.h"
#include "reservation.h"

FILE * fpassengers;
FILE * fscFlight;





/**************************************************************************************************************************/
/**************************************************************************************
 *   Function to make a reservation
 *   by choosing a passenger and scheduled flight
 **************************************************************************************/
void makeReservationExistingP(struct Passenger *passenger, struct Flight *scheduledFlight, short n, short newPass)
{
    short count;    // declare variable
    short optionP, countF, optionR;  // opetionP stores user's choise of passenger, countF to store number of records in scheduled flight file, opetionR store user's choise of flight

    fflush(stdin);       // unespected behaviour prevention

    // if passenger and flight file file can be opened, and there is at least one passenger in file and user input is not -1
    // there is at least one schedule flight in a file, proceed
    if(openPassengerFile("r+b") && openfscFlightFile("r+b"))
    if( n || ((count = listPassengers(passenger)) != -1 && (optionP = get_input(-1, count)) != -1))
    if((countF = listFlights(scheduledFlight)) != -1)
    {
        short ok = 0; // when it is 1, loop will stop

        // if n is 1(which indicates that reservation for new passenger is being made)
        // optionP takes value of passengerIndex (makeReservationNewP function)
        if(n)   optionP = newPass;

        do
        {
            printf("\nYour input: ");

            // get user input and proceed if not -1
            if((optionR  = get_input(-1, countF)) == -1) break;
            else
            {
                // if choosen flight is cancelled, reservation cannot be made
                if(strcmp(scheduledFlight[optionR].status, "CANCELLED" ) == 0)
                    puts("This flight is cancelled and you cannot make a reservation for it.\n"
                         "Choose another or -1 to exit.");
                else if(scheduledFlight[optionR].availableSeats < 1)        // or if all seats are booked
                    puts("There are no more available seats in this flight.\nChoose another or -1 to exit.");
                else
                {
                    // if not, check whether a reservation on that flight is already being made by this passenger
                    for(count = 0; count < MAXPASSENGERS; count++)
                    {
                        if(passenger[optionP].reservations[count] == optionR)
                        {
                            puts("You already have a reservation for this flight.\n"
                                 "Choose another or -1 to exit");
                            break;
                        }
                        else if(passenger[optionP].reservations[count] == -1)    // if free element is reached, make reservation
                        {
                            ok = 1;

                            // store chosen flight to a location where is first free element found
                            passenger[optionP].reservations[findFreeElement(passenger, optionP)] = optionR; // make reservation
                            scheduledFlight[optionR].availableSeats--;      // decrease number of available seats
                            system("cls");
                            //show reservation
                            puts("Your reservation:");
                            show_FlightInfo(optionR, scheduledFlight);  // print booked filght

                            short sizeP = sizeof (struct Passenger);  // get seize of structure Passenger
                            short sizeF = sizeof (struct Flight);     // get seize of structure Passenger

                            fseek(fpassengers, sizeP * optionP, SEEK_SET);      // find position of record with chosen passenger
                            fwrite(&passenger[optionP], sizeP, 1, fpassengers); // write there edited data

                            fseek(fscFlight, sizeF * optionR, SEEK_SET);        // find position of record with chosen passenger
                            fwrite(&scheduledFlight[optionR], sizeF, 1, fscFlight);   // write there edited data
                            break;  // break the loop
                        }

                    }
                }
            }
        }while(ok != 1);   // while free element is not reached
    }   // inner if
    fclose(fscFlight);      // close file
    fclose(fpassengers);    // when done, close file
}
/***************************************************************************************************************************/





/**************************************************************************************************************************/

/**************************************************************************************
 *   Function to make a reservation for new passenger
 **************************************************************************************/
void makeReservationNewP(struct Passenger *passenger, struct Flight *scheduledFlight)
{
    openPassengerFile("r+b");       // open fpassenger file

    short passengerIndex = listPassengers(passenger) + 1;   // calculate number of passengers

    system("cls");

    if(createNewPassenger(passenger, 1))    // create new passener, if file is full or something else occured, skip reservation
        makeReservationExistingP(passenger, scheduledFlight, 1, passengerIndex); // make reservation for new passenger

    fclose(fpassengers);    // close file
}
/**************************************************************************************************************************/





/**************************************************************************************************************************/

/**************************************************************************************
 *   Function to display all passenger's past reservations
 **************************************************************************************/
void show_reservations(const struct Passenger *passenger, const struct Flight *scheduledFlight)
{

    short count;    // declare counter
    fflush(stdin);// unespected behaviour prevention
    short optionP;  // get user input, optionP is to choose passenger which history we want

    // if files can be opened and there is at least one passenger, and one scheduled flight, proceed
    if(openPassengerFile("rb") && openfscFlightFile("rb") && (count = listPassengers(passenger)) != -1 && ((optionP = get_input(-1, count)) != -1))
    {
        count = 0;                                                           // reset counter
        short sizeF = sizeof (struct Flight);                                // get size of Flight structure

        // while there is data to be read, read from file and store in structure
        while (fread(&scheduledFlight[count], sizeF, 1, fscFlight) == 1)
            count++;             // increment counter

        system("cls");

        // loop trough passenger's reservations
        for( count = 0; count < MAXPASSENGERS; count++)
        {

            if(passenger[optionP].reservations[count] == -1)    // when free element(-1) is read
                break;                                          // exit loop

            if(count == 0)                                      // show message only once if there are any reservations
                puts("All passenger's past reservations are: ");

            show_FlightInfo(passenger[optionP].reservations[count], scheduledFlight); // show reservation
        }

        if(count == 0) puts("No reservations by this passenger.");  // if there are no reservations
    }
    fclose(fscFlight);
    fclose(fpassengers);
}
/***************************************************************************************************************************/





/**************************************************************************************************************************/

/*****************************************************************************************
 *   Function loop trough array to find element -1
 *   -1 indicates that there is no data written (data is reference to a schedule flight)
 *****************************************************************************************/
short findFreeElement(struct Passenger *passenger, short n)
{
    short i;                                // declare counter;

    for ( i = 0; i < MAXPASSENGERS; i++)    // loop trough loop
    {
        if(passenger[n].reservations[i] == -1) // if free element is found
            break;                             // exit loop
    }

    return i;   // return index of free element
}
/***************************************************************************************************************************/






/**************************************************************************************************************************/
/****************************************************************************************************
 *   Function reinisialize reservations to -1
 *   if file or content of the flight file is deleted accidentally or on purpose
 ****************************************************************************************************/
 void iniPassRes(struct Passenger *passenger, const struct Fligh *scheduledFlight)
{
    short count, index;
    int size = sizeof(struct Passenger);

    if(!openfscFlightFile("rb") || (count = listFlights(scheduledFlight) == -1))    // if file or its content is deleted
    {

        count = 0;
        if(openPassengerFile("r+b"))        // if file can be opened
        while (fread(&passenger[count], size, 1, fpassengers) == 1)
        {
            for(index = 0; index < MAXPASSENGERS; index++)
            {
                passenger[count].reservations[index] = -1;                  // reset reservation to -1(which indicates no reservations
                if(passenger[count].reservations[index + 1] == -1) break;   // if -1 is next element, break the loop
            }
            count++; // increment counter
        }

        rewind(fpassengers);    // go at the start of a file
        fwrite(&passenger[0], size, count, fpassengers);   // write edited data
    }

    fclose(fscFlight);
    fclose(fpassengers);
    system("cls");
}
/**************************************************************************************************************************/





/**************************************************************************************************************************/
/**************************************************************************************
 *   Function to show user interface main menu
 **************************************************************************************/
void menu(void)
{

    short option;     // variable to store user input

    // printing main menu options
    do
    {
        iniPassRes(passenger, scheduledFlight);
        printf("*--------------------WELCOME-TO-AIRLINE-RESERVATION-SYSTEM--------------------*\n\n");
        printf("*-----------------------------------Flights-----------------------------------*\n\n");
        printf("\t0. Create\t1. Modify\t2. Cancel\t3. Show All\n\n\n");
        printf("*----------------------------------Passenger----------------------------------*\n\n");
        printf("\t4. Create   5. Modify   6. Show All   7. View Reservation History\n\n\n");
        printf("*-------------------------------Reservation-for-------------------------------*\n\n");
        printf("\t8. New Passenger\t9. Existing passenger\n\n");
        printf("*-----------------------------------------------------------------------------*\n\n");
        printf("-1. Exit\n\n");
        printf("Your input: ");

        option = get_input(-1, 9);
        system("cls");      // clear screen

        // depending on input,
        switch(option)
        {
            case 0:  createFlight(scheduledFlight);                                      break; // create new scheduled flight(s)
            case 1:  modifyFlight(scheduledFlight);                                      break; // show all existing scheduled flights
            case 2:  cancelFlight(scheduledFlight);                                      break; // cancel scheduled flight
            case 3:  show_existingFlights(scheduledFlight);                              break; // show all scheduled flights
            case 4:  createNewPassenger(passenger, 0);                                   break; // create new passenger(s)
            case 5:  modifyPassenger(passenger);                                         break; // show all passengers
            case 6:  show_existingPassengers(passenger);                                 break; // modify passenger
            case 7:  show_reservations(passenger, scheduledFlight);                      break; // show passenger's past reservations
            case 8:  makeReservationNewP(passenger, scheduledFlight);                    break; // make new passenger/s and make reservations for them
            case 9:  makeReservationExistingP(passenger, scheduledFlight, 0, 0);         break; // make reservation for existing passenger
            case -1: puts("Thank you for using our airline ticket reservation system."); break; // exit the program
            default: puts("Invalid input, please provide a number:");                    break; // invalid input
        }

        system("pause");     // pause application
        system("cls");       // clear screen

    }while(option != -1);    // while input is not zero

}

/**************************************************************************************************************************/
