#pragma once

class Utils
{
public:
	static const unsigned int crc32_table[];
	static unsigned int xcrc32(const unsigned char* buf, int len, unsigned int init);
};