﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaitai
{
    public partial class Ipv6Packet : KaitaiStruct
    {
        public static Ipv6Packet FromFile(string fileName)
        {
            return new Ipv6Packet(new KaitaiStream(fileName));
        }

        public Ipv6Packet(KaitaiStream p__io, KaitaiStruct p__parent = null, Ipv6Packet p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _version = m_io.ReadBitsInt(4);
            _trafficClass = m_io.ReadBitsInt(8);
            _flowLabel = m_io.ReadBitsInt(20);
            m_io.AlignToByte();
            _payloadLength = m_io.ReadU2be();
            _nextHeaderType = m_io.ReadU1();
            _hopLimit = m_io.ReadU1();
            _srcIpv6Addr = m_io.ReadBytes(16);
            _dstIpv6Addr = m_io.ReadBytes(16);
            switch (NextHeaderType)
            {
                case 17:
                    {
                        _nextHeader = new UdpDatagram(m_io);
                        break;
                    }
                case 0:
                    {
                        _nextHeader = new OptionHopByHop(m_io, this, m_root);
                        break;
                    }
                case 4:
                    {
                        _nextHeader = new Ipv4Packet(m_io);
                        break;
                    }
                case 6:
                    {
                        _nextHeader = new TcpSegment(m_io);
                        break;
                    }
                case 59:
                    {
                        _nextHeader = new NoNextHeader(m_io, this, m_root);
                        break;
                    }
            }
            _rest = m_io.ReadBytesFull();
        }
        public partial class NoNextHeader : KaitaiStruct
        {
            public static NoNextHeader FromFile(string fileName)
            {
                return new NoNextHeader(new KaitaiStream(fileName));
            }

            public NoNextHeader(KaitaiStream p__io, KaitaiStruct p__parent = null, Ipv6Packet p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
            }
            private Ipv6Packet m_root;
            private KaitaiStruct m_parent;
            public Ipv6Packet M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        public partial class OptionHopByHop : KaitaiStruct
        {
            public static OptionHopByHop FromFile(string fileName)
            {
                return new OptionHopByHop(new KaitaiStream(fileName));
            }

            public OptionHopByHop(KaitaiStream p__io, KaitaiStruct p__parent = null, Ipv6Packet p__root = null) : base(p__io)
            {
                m_parent = p__parent;
                m_root = p__root;
                _read();
            }
            private void _read()
            {
                _nextHeaderType = m_io.ReadU1();
                _hdrExtLen = m_io.ReadU1();
                _body = m_io.ReadBytes((HdrExtLen - 1));
                switch (NextHeaderType)
                {
                    case 0:
                        {
                            _nextHeader = new OptionHopByHop(m_io, this, m_root);
                            break;
                        }
                    case 6:
                        {
                            _nextHeader = new TcpSegment(m_io);
                            break;
                        }
                    case 59:
                        {
                            _nextHeader = new NoNextHeader(m_io, this, m_root);
                            break;
                        }
                }
            }
            private byte _nextHeaderType;
            private byte _hdrExtLen;
            private byte[] _body;
            private KaitaiStruct _nextHeader;
            private Ipv6Packet m_root;
            private KaitaiStruct m_parent;
            public byte NextHeaderType { get { return _nextHeaderType; } }
            public byte HdrExtLen { get { return _hdrExtLen; } }
            public byte[] Body { get { return _body; } }
            public KaitaiStruct NextHeader { get { return _nextHeader; } }
            public Ipv6Packet M_Root { get { return m_root; } }
            public KaitaiStruct M_Parent { get { return m_parent; } }
        }
        private ulong _version;
        private ulong _trafficClass;
        private ulong _flowLabel;
        private ushort _payloadLength;
        private byte _nextHeaderType;
        private byte _hopLimit;
        private byte[] _srcIpv6Addr;
        private byte[] _dstIpv6Addr;
        private KaitaiStruct _nextHeader;
        private byte[] _rest;
        private Ipv6Packet m_root;
        private KaitaiStruct m_parent;
        public ulong Version { get { return _version; } }
        public ulong TrafficClass { get { return _trafficClass; } }
        public ulong FlowLabel { get { return _flowLabel; } }
        public ushort PayloadLength { get { return _payloadLength; } }
        public byte NextHeaderType { get { return _nextHeaderType; } }
        public byte HopLimit { get { return _hopLimit; } }
        public byte[] SrcIpv6Addr { get { return _srcIpv6Addr; } }
        public byte[] DstIpv6Addr { get { return _dstIpv6Addr; } }
        public KaitaiStruct NextHeader { get { return _nextHeader; } }
        public byte[] Rest { get { return _rest; } }
        public Ipv6Packet M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
