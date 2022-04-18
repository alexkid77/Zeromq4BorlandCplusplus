//---------------------------------------------------------------------------

#include <vcl.h>
#pragma hdrstop

#include "Unit1.h"
#include "zmq.hpp"
#include <Windows.h>
//---------------------------------------------------------------------------
#pragma package(smart_init)
#pragma resource "*.dfm"
TForm1 *Form1;
//---------------------------------------------------------------------------
__fastcall TForm1::TForm1(TComponent* Owner)
	: TForm(Owner)
{
	  /*	 void *context = zmq_ctx_new ();
			   void *responder = zmq_socket (context, ZMQ_REP);
    int rc = zmq_bind (responder, "tcp://*:5555");
    assert (rc == 0);

	while (1) {
		char buffer [10];
		zmq_recv (responder, buffer, 10, 0);
		printf ("Received Hello\n");
		Sleep (1);          //  Do some 'work'
		zmq_send (responder, "World", 5, 0);
	}               */


	 zmq::context_t context{1};

    // construct a REP (reply) socket and bind to interface
	zmq::socket_t socket{context, zmq::socket_type::radio};
	socket.bind("udp://*:5555");

	// prepare some static data for responses
	const std::string data{"World"};

	for (;;)
    {
	 socket.send(zmq::str_buffer("A"), zmq::send_flags::sndmore);
	 socket.send(zmq::str_buffer("Message in A envelope"));
	 Sleep (1);
	}


}
//---------------------------------------------------------------------------
